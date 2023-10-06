using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ConversationService : IConversationService
{
    private readonly ILogger<ContractService> _logger;
    private readonly IFAMService _famService;
    private readonly IConversationRepository _conversationRepository;
    private readonly IAzureUserCacheService _azureUserCacheService;
    private readonly IFusionService _fusionService;

    public ConversationService(ILoggerFactory loggerFactory,
        IConversationRepository conversationRepository,
        IAzureUserCacheService azureUserCacheService,
        IFusionService fusionService,
        IFAMService famService)
    {
        _logger = loggerFactory.CreateLogger<ContractService>();
        _conversationRepository = conversationRepository;
        _azureUserCacheService = azureUserCacheService;
        _fusionService = fusionService;
        _famService = famService;
    }

    public async Task<Conversation> CreateConversation(Conversation conversation)
    {
        // TODO: Not sure, how to verify the project from the tag. 
        // This needs to be relook when we have integration to FAM
        _ = await _famService.GetTagData(conversation.TagNo)
            ?? throw new NotFoundException("Invalid tag data");
        return await _conversationRepository.CreateConversation(conversation);
    }

    public async Task<Conversation> GetConversation(Guid conversationId)
    {
        var conversation = await _conversationRepository.GetConversation(conversationId);
        return conversation ??
            throw new NotFoundException($"Unable to find conversation for the conversationId - {conversationId} not found");
    }

    public async Task<List<Conversation>> GetConversations(Guid projectId, string tagNo, bool includeLatestMessage)
    {
        if (includeLatestMessage)
        {
            return await _conversationRepository.GetConversationsWithLatestMessage(projectId, tagNo, false);
        }
        return await _conversationRepository.GetConversations(projectId, tagNo);
    }

    public async Task<Message> AddMessage(Guid conversationId, Message message)
    {
        var conversation = await _conversationRepository.GetConversation(conversationId)
                ?? throw new Exception("Invalid conversation");

        message.SetConversation(conversation);

        return await _conversationRepository.AddMessage(message);
    }


    public async Task<List<Message>> GetMessages(Guid converstionId)
    {
        var messages = await _conversationRepository.GetMessages(converstionId);
        return messages;
    }

    public async Task<Message> GetMessage(Guid messageId)
    {
        var message = await _conversationRepository.GetMessage(messageId);
        return message ??
            throw new NotFoundException($"Unable to find message for the message Id - {messageId}.");
    }

    public async Task DeleteMessage(Guid oldMessageId, Guid azureUniqueId)
    {
        if (azureUniqueId == Guid.Empty) { throw new BadRequestException("Invalid azure unique id"); }
        var existingMessage = await _conversationRepository.GetMessage(oldMessageId)
                    ?? throw new NotFoundException("Invalid message id");
        if (existingMessage.UserId != azureUniqueId)
        {
            throw new BadRequestException("User not author of this message");
        }
        if (existingMessage.SoftDeleted) { throw new BadRequestException("Cannot update deleted message"); }

        existingMessage.SoftDeleted = true;
        await _conversationRepository.UpdateMessage(existingMessage);
    }

    public async Task<Message> UpdateMessage(Guid messageId, Message updatedMessage)
    {
        var existingMessage = await _conversationRepository.GetMessage(messageId)
                ?? throw new NotFoundException($"Message with id {messageId} not found");

        if (existingMessage.UserId != updatedMessage.UserId)
        {
            throw new BadRequestException("User not author of this Message");
        }

        existingMessage.Text = updatedMessage.Text;
        existingMessage.IsEdited = true;
        return await _conversationRepository.UpdateMessage(existingMessage);
    }

    public async Task<string> GetUserName(Guid userId)
    {
        var azureUser = await _azureUserCacheService.GetAzureUserAsync(userId);
        if (azureUser == null)
        {
            var user = await _fusionService.ResolveUserFromPersonId(userId);
            if (user != null)
            {
                azureUser = new AzureUser { AzureUniqueId = userId, Name = user?.Name };
                _azureUserCacheService.AddAzureUser(azureUser);
            }
        }
        if (azureUser != null)
        {
            return azureUser.Name ?? "Unknown user";
        }
        else
        {
            throw new NotFoundException("Unable to find the username for the userId: " + userId);
        }
    }

    public async Task<Dictionary<Guid, string>> GetUserIdUserName(List<Guid> userIds)
    {
        var userIdUserNameMap = new Dictionary<Guid, string>();
        foreach (Guid userId in userIds)
        {
            var userName = await GetUserName(userId);
            userIdUserNameMap.TryAdd(userId, userName);
        }
        return userIdUserNameMap;
    }
}
