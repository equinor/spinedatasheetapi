using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ConversationService : IConversationService
{
    private readonly ILogger<ContractService> _logger;
    private readonly ITagDataReviewService _tagDataReviewService;
    private readonly IConversationRepository _conversationRepository;
    private readonly IAzureUserCacheService _azureUserCacheService;
    private readonly IFusionService _fusionService;

    public ConversationService(ILoggerFactory loggerFactory,
        IConversationRepository conversationRepository,
        IAzureUserCacheService azureUserCacheService,
        IFusionService fusionService,
        ITagDataReviewService tagDataReviewService)
    {
        _logger = loggerFactory.CreateLogger<ContractService>();
        _conversationRepository = conversationRepository;
        _azureUserCacheService = azureUserCacheService;
        _fusionService = fusionService;
        _tagDataReviewService = tagDataReviewService;
    }

    public async Task<Conversation> CreateConversation(Conversation conversation)
    {
        var tagDataReview = await _tagDataReviewService.GetTagDataReview(
                (Guid)conversation.TagDataReviewId)
        ?? throw new Exception("Invalid tag data review");

        conversation.SetTagDataReview(tagDataReview);

        return await _conversationRepository.CreateConversation(conversation);
    }

    public async Task<Conversation> GetConversation(Guid conversationId)
    {
        var conversation = await _conversationRepository.GetConversation(conversationId);
        return conversation ??
            throw new NotFoundException($"Unable to find conversation for the conversationId - {conversationId} not found");
    }

    public async Task<List<Conversation>> GetConversations(Guid reviewId)
    {
        var conversations = await _conversationRepository.GetConversations(reviewId);
        return conversations;
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
