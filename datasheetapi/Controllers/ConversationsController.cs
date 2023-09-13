using datasheetapi.Adapters;

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
        [FromRoute] Guid reviewId, [FromBody] ConversationDto conversation)
    {
        if (conversation.Property != null)
        {
            if (!ValidateProperty<InstrumentPurchaserRequirement>(conversation.Property) &&
               !ValidateProperty<InstrumentSupplierOfferedProduct>(conversation.Property))
            {
                return BadRequest($"Not supported property: {conversation.Property}");
            }
        }

        var azureUniqueId = GetAzureUniqueId();
        try
        {
            var savedConversation = await _conversationService.CreateConversation(
                conversation.ToModel(reviewId, azureUniqueId));

            var userIdNameMap = await _conversationService.GetUserIdUserName(
                savedConversation.Participants.Select(p => p.UserId).ToList());

            return savedConversation.ToDto(userIdNameMap);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating the conversation");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{conversationId}", Name = "GetConversation")]
    public async Task<ActionResult<GetConversationDto>> GetConversation(Guid conversationId)
    {
        if (conversationId == Guid.Empty)
        {
            return BadRequest();
        }
        try
        {
            var conversation = await _conversationService.GetConversation(conversationId);
            if (conversation == null)
            {
                return NotFound();
            }
            var userIdNameMap = await _conversationService.GetUserIdUserName(
                conversation.Participants.Select(p => p.UserId).ToList());

            return conversation.ToDto(userIdNameMap);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting conversation with id {id}", conversationId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(Name = "GetConversations")]
    public async Task<ActionResult<List<GetConversationDto>>> GetConversations(Guid reviewId)
    {
        try
        {
            var conversations = await _conversationService.GetConversations(reviewId);

            var userIds = conversations.SelectMany(conversation =>
                            conversation.Participants.Select(p => p.UserId)).ToList();
            var userIdNameMap = await _conversationService.GetUserIdUserName(userIds);

            return conversations.Select(conversation => conversation.ToDto(userIdNameMap)).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all conversations");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("{conversationId}/messages", Name = "AddMessage")]
    public async Task<ActionResult<GetMessageDto>> AddMessage([FromRoute] Guid reviewId,
         [FromRoute] Guid conversationId, MessageDto messageDto)
    {

        var message = messageDto.ToMessageModel(GetAzureUniqueId());

        var savedMessage = await _conversationService.AddMessage(conversationId, message);

        return savedMessage.ToMessageDto(await _conversationService.GetUserName(savedMessage.UserId));
    }

    [HttpGet("{conversationId}/messages/{messageId}", Name = "GetMessage")]
    public async Task<ActionResult<GetMessageDto>> GetMessage(Guid messageId)
    {
        if (messageId == Guid.Empty)
        {
            return BadRequest();
        }
        try
        {
            var message = await _conversationService.GetMessage(messageId);
            if (message == null)
            {
                return NotFound();
            }
            var username = await _conversationService.GetUserName(message.UserId);
            return message.ToMessageDto(username);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting message with id {id}", messageId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{conversationId}/messages", Name = "GetMessages")]
    public async Task<ActionResult<List<GetMessageDto>>> GetMessages(Guid conversationId)
    {
        if (conversationId == Guid.Empty)
        {
            return BadRequest();
        }
        try
        {
            var messges = await _conversationService.GetMessages(conversationId);

            var userIdNameMap = await _conversationService.GetUserIdUserName(
                    messges.Select(c => c.UserId).ToList());

            return messges.ToMessageDtos(userIdNameMap);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting message with conversation id {id}", conversationId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{conversationId}/messages/{messageId}", Name = "UpdateMessage")]
    public async Task<ActionResult<GetMessageDto>> UpdateMessage([FromRoute] Guid conversationId,
                                                            [FromRoute] Guid messageId,
                                                            MessageDto newMessageDto)
    {
        if (messageId == Guid.Empty || newMessageDto == null)
        {
            return BadRequest();
        }
        try
        {
            var newMessage = newMessageDto.ToMessageModel(GetAzureUniqueId());

            var message = await _conversationService.UpdateMessage(messageId, newMessage);

            var userName = await _conversationService.GetUserName(message.UserId);
            return message.ToMessageDto(userName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error editing message", messageId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{conversationId}/message/{messageId}", Name = "DeleteMessage")]
    public async Task<ActionResult> DeleteMessage([FromRoute] Guid conversationId,
                                                [FromRoute] Guid messageId)
    {
        var azureUniqueId = GetAzureUniqueId();

        if (messageId == Guid.Empty)
        {
            return BadRequest();
        }

        try
        {
            await _conversationService.DeleteMessage(messageId, azureUniqueId);
            return NoContent();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting message", messageId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
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
        var propertyInfo = obj.GetType().GetProperty(propertyName);

        return propertyInfo != null;
    }
}
