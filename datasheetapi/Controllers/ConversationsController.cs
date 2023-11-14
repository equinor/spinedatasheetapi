using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;
using datasheetapi.Dtos.Conversation;
using datasheetapi.Exceptions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("/projects/{projectId}/tags/{tagNo}/conversations")]
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
    private readonly IUserService _userService;
    private readonly ILogger<ConversationsController> _logger;

    public ConversationsController(
        ILoggerFactory loggerFactory,
        IConversationService conversationService,
        IUserService userService
        )
    {
        _logger = loggerFactory.CreateLogger<ConversationsController>();
        _conversationService = conversationService;
        _userService = userService;
    }

    [HttpPost(Name = "CreateConversation")]
    public async Task<ActionResult<GetConversationDto>> CreateConversation(
        [FromRoute][NotEmptyGuid] Guid projectId,
        [FromRoute][Required] string tagNo,
        [FromBody][Required] ConversationDto conversation)
    {
        _logger.LogDebug("Creating new conversation in the tagNo {tagNo}.", tagNo);
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
            conversation.ToModel(projectId, tagNo, Utils.GetAzureUniqueId(HttpContext.User)));
        _logger.LogInformation(
                "Created new conversation in tag {tagNo} & project {projectId}.", tagNo, projectId);

        var userIdNameMap = await _userService.GetDisplayNames(
            savedConversation.Participants.Select(p => p.UserId).ToList());
        return savedConversation.ToDto(userIdNameMap);
    }

    [HttpPut("{conversationId}", Name = "UpdateConversation")]
    public async Task<ActionResult<GetConversationDto>> UpdateConversation(
        Guid projectId,
        [Required] string tagNo,
        Guid conversationId,
        [Required] UpdateConversationDto conversation)
    {
        var savedConversation = await _conversationService.UpdateConversation(conversationId, ConversationAdapter.MapConversationStatusDTOToModel(conversation.ConversationStatus));

        var userIdNameMap = await _userService.GetDisplayNames(
            savedConversation.Participants.Select(p => p.UserId).ToList());

        return savedConversation.ToDto(userIdNameMap);
    }

    [HttpGet("{conversationId}", Name = "GetConversation")]
    public async Task<ActionResult<GetConversationDto>> GetConversation(
        [NotEmptyGuid] Guid projectId, [Required] string tagNo, [NotEmptyGuid] Guid conversationId)
    {
        _logger.LogDebug("Fetching conversation for tagNo {tagNo} & project {projectId}", tagNo, projectId);
        var conversation = await _conversationService.GetConversation(conversationId);

        var userIdNameMap = await _userService.GetDisplayNames(
            conversation.Participants.Select(p => p.UserId).ToList());

        return conversation.ToDto(userIdNameMap);

    }

    /// <summary>
    /// Get the list of conversation available under the reviewId
    /// </summary>
    /// <param name="reviewId">Unique Id for the review</param>
    /// <param name="includeLatestMessage">Include Latest Message in the conversation.
    /// The latest message will be non soft deleted message if at least one exists, else it will send last soft deleted message.</param>
    /// <returns></returns>
    [HttpGet(Name = "GetConversations")]
    public async Task<ActionResult<List<GetConversationDto>>> GetConversations([NotEmptyGuid] Guid projectId,
        [Required] string tagNo, [FromQuery] bool includeLatestMessage = false)
    {

        var conversations = await _conversationService.GetConversations(projectId, tagNo, includeLatestMessage);

        var userIds = conversations.SelectMany(conversation =>
                        conversation.Participants.Select(p => p.UserId)).ToList();
        var userIdNameMap = await _userService.GetDisplayNames(userIds);

        return conversations.Select(conversation => conversation.ToDto(userIdNameMap)).ToList();
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
