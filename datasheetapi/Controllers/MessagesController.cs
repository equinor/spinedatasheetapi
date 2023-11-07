using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;
using datasheetapi.Dtos.Conversation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("/conversations")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class MessagesController : ControllerBase
{
    private readonly IConversationService _conversationService;
    private readonly IUserService _userService;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(
        ILoggerFactory loggerFactory,
        IConversationService conversationService,
        IUserService userService
        )
    {
        _logger = loggerFactory.CreateLogger<MessagesController>();
        _conversationService = conversationService;
        _userService = userService;
    }

    [HttpPost("{conversationId}/messages", Name = "AddMessage")]
    public async Task<ActionResult<GetMessageDto>> AddMessage(
        [FromRoute][NotEmptyGuid] Guid conversationId, [Required] MessageDto messageDto)
    {
        _logger.LogDebug("Adding new message in the {conversationId}", conversationId);
        var message = messageDto.ToMessageModel(Utils.GetAzureUniqueId(HttpContext.User));

        var savedMessage = await _conversationService.AddMessage(conversationId, message);
        _logger.LogInformation("Added new message in the conversation {conversationId}.", conversationId);

        return savedMessage.ToMessageDto(await _userService.GetDisplayName(savedMessage.UserId));
    }

    [HttpGet("{conversationId}/messages/{messageId}", Name = "GetMessage")]
    public async Task<ActionResult<GetMessageDto>> GetMessage(
        [NotEmptyGuid] Guid conversationId, [NotEmptyGuid] Guid messageId)
    {
        _logger.LogDebug("Fetching message on the conversation {conversationId}.", conversationId);
        var message = await _conversationService.GetMessage(messageId);
        var username = await _userService.GetDisplayName(message.UserId);

        return message.ToMessageDto(username);
    }

    [HttpGet("{conversationId}/messages", Name = "GetMessages")]
    public async Task<ActionResult<List<GetMessageDto>>> GetMessages([NotEmptyGuid] Guid conversationId)
    {
        _logger.LogDebug("Fetching messages on the conversation {conversationId}.", conversationId);
        var messges = await _conversationService.GetMessages(conversationId);

        var userIdNameMap = await _userService.GetDisplayNames(
                messges.Select(c => c.UserId).ToList());

        return messges.ToMessageDtos(userIdNameMap);
    }

    [HttpPut("{conversationId}/messages/{messageId}", Name = "UpdateMessage")]
    public async Task<ActionResult<GetMessageDto>> UpdateMessage(
        [FromRoute][NotEmptyGuid] Guid conversationId, [FromRoute][NotEmptyGuid] Guid messageId,
        [Required] MessageDto newMessageDto)
    {
        _logger.LogDebug("Updating the message {messageId}.", messageId);
        var newMessage = newMessageDto.ToMessageModel(Utils.GetAzureUniqueId(HttpContext.User));

        var message = await _conversationService.UpdateMessage(messageId, newMessage);
        _logger.LogInformation("Updated the message {messageId} on the conversation {conversationId}.",
            messageId, conversationId);

        var userName = await _userService.GetDisplayName(message.UserId);
        return message.ToMessageDto(userName);

    }

    [HttpDelete("{conversationId}/messages/{messageId}", Name = "DeleteMessage")]
    public async Task<ActionResult> DeleteMessage(
        [FromRoute][NotEmptyGuid] Guid conversationId, [FromRoute][NotEmptyGuid] Guid messageId)
    {
        _logger.LogDebug("Deleting the message {messageId} on conversation {conversationId}.", messageId, conversationId);
        await _conversationService.DeleteMessage(messageId, Utils.GetAzureUniqueId(HttpContext.User));
        _logger.LogInformation("Deleted the message {messageId} on conversation {conversationId}.",
            messageId, conversationId);

        return NoContent();
    }
}
