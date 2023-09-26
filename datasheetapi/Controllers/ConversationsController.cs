using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;
using datasheetapi.Exceptions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("/tag/reviews/{reviewId}/conversations")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class ConversationsController : ControllerBase
{
    private readonly IConversationService _conversationService;
    private readonly ILogger<ConversationsController> _logger;

    public ConversationsController(ILoggerFactory loggerFactory,
                            IConversationService conversationService)
    {
        _logger = loggerFactory.CreateLogger<ConversationsController>();
        _conversationService = conversationService;
    }

    [HttpPost(Name = "CreateConversation")]
    public async Task<ActionResult<GetConversationDto>> CreateConversation(
        [FromRoute] [NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid reviewId,
        [FromBody] [Required] ConversationDto conversation)
    {
        _logger.LogDebug("Creating new conversation in the review {reviewId}.", reviewId);
        if (conversation.Property != null)
        {
            if (!ValidateProperty<InstrumentPurchaserRequirement>(conversation.Property) &&
                !ValidateProperty<InstrumentSupplierOfferedProduct>(conversation.Property) &&
                !ValidateProperty<TagDataDto>(conversation.Property))
            {
                throw new BadRequestException($"Not supported property: {conversation.Property}");
            }
        }

        var savedConversation = await _conversationService.CreateConversation(
            conversation.ToModel(reviewId, GetAzureUniqueId()));
        _logger.LogInformation("Created new conversation in the review {reviewId}.", reviewId);

        var userIdNameMap = await _conversationService.GetUserIdUserName(
            savedConversation.Participants.Select(p => p.UserId).ToList());
        return savedConversation.ToDto(userIdNameMap);
    }

    [HttpGet("{conversationId}", Name = "GetConversation")]
    public async Task<ActionResult<GetConversationDto>> GetConversation(
        [NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid conversationId)
    {
        var conversation = await _conversationService.GetConversation(conversationId);

        var userIdNameMap = await _conversationService.GetUserIdUserName(
            conversation.Participants.Select(p => p.UserId).ToList());

        return conversation.ToDto(userIdNameMap);

    }

    [HttpGet(Name = "GetConversations")]
    public async Task<ActionResult<List<GetConversationDto>>> GetConversations(
        [NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid reviewId)
    {

        var conversations = await _conversationService.GetConversations(reviewId);

        var userIds = conversations.SelectMany(conversation =>
                        conversation.Participants.Select(p => p.UserId)).ToList();
        var userIdNameMap = await _conversationService.GetUserIdUserName(userIds);

        return conversations.Select(conversation => conversation.ToDto(userIdNameMap)).ToList();
    }

    [HttpPost("{conversationId}/messages", Name = "AddMessage")]
    public async Task<ActionResult<GetMessageDto>> AddMessage(
        [FromRoute][NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid conversationId,
        [Required] MessageDto messageDto)
    {
        _logger.LogDebug("Adding new message in the conversation {conversationId}.", conversationId);
        var message = messageDto.ToMessageModel(GetAzureUniqueId());

        var savedMessage = await _conversationService.AddMessage(conversationId, message);
        _logger.LogInformation("Added new message in the conversation {conversationId}.", conversationId);

        return savedMessage.ToMessageDto(await _conversationService.GetUserName(savedMessage.UserId));
    }

    [HttpGet("{conversationId}/messages/{messageId}", Name = "GetMessage")]
    public async Task<ActionResult<GetMessageDto>> GetMessage(
        [NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid messageId)
    {
        var message = await _conversationService.GetMessage(messageId);
        var username = await _conversationService.GetUserName(message.UserId);

        return message.ToMessageDto(username);
    }

    [HttpGet("{conversationId}/messages", Name = "GetMessages")]
    public async Task<ActionResult<List<GetMessageDto>>> GetMessages(
        [NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid conversationId)
    {
        var messges = await _conversationService.GetMessages(conversationId);

        var userIdNameMap = await _conversationService.GetUserIdUserName(
                messges.Select(c => c.UserId).ToList());

        return messges.ToMessageDtos(userIdNameMap);
    }

    [HttpPut("{conversationId}/messages/{messageId}", Name = "UpdateMessage")]
    public async Task<ActionResult<GetMessageDto>> UpdateMessage(
        [FromRoute][NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid conversationId,
        [FromRoute][NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid messageId,
        [Required] MessageDto newMessageDto)
    {
        _logger.LogDebug("Updating the message {messageId}.", messageId);
        var newMessage = newMessageDto.ToMessageModel(GetAzureUniqueId());

        var message = await _conversationService.UpdateMessage(messageId, newMessage);
        _logger.LogInformation("Updated the message {messageId} on conversation {conversationId}.", messageId, conversationId);

        var userName = await _conversationService.GetUserName(message.UserId);
        return message.ToMessageDto(userName);

    }

    [HttpDelete("{conversationId}/messages/{messageId}", Name = "DeleteMessage")]
    public async Task<ActionResult> DeleteMessage(
        [FromRoute][NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid conversationId,
        [FromRoute][NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid messageId)
    {
        _logger.LogDebug("Deleting the message {messageId} on conversation {conversationId}.", messageId, conversationId);
        await _conversationService.DeleteMessage(messageId, GetAzureUniqueId());
        _logger.LogInformation("Deleted the message {messageId} on conversation {conversationId}.", messageId, conversationId);

        return NoContent();
    }

    private Guid GetAzureUniqueId()
    {
        var httpContext = HttpContext;
        var user = httpContext.User;
        var fusionIdentity = user.Identities.FirstOrDefault(i => i is Fusion.Integration.Authentication.FusionIdentity) as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ?? throw new Exception("Could not get Azure Unique Id");
        return azureUniqueId;
    }

    private static bool ValidateProperty<T>(string propertyName)
    where T : class, new()
    {
        var obj = new T();
        var propertyInfo = obj.GetType().GetProperty(
            propertyName,
            System.Reflection.BindingFlags.IgnoreCase |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance
            );

        return propertyInfo != null;
    }
}
