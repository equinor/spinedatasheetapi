using datasheetapi.Adapters;
using datasheetapi.Dtos.Conversation;
using datasheetapi.Exceptions;

using Fusion;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("containers")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class ContainersController : ControllerBase
{
    private readonly IContainerService _containerService;
    private readonly IConversationService _conversationService;
    private readonly ILogger<ContractsController> _logger;
    private readonly IUserService _userService;

    public ContainersController(
        ILoggerFactory loggerFactory,
        IContainerService containerService,
        IConversationService conversationService,
        IUserService userService
        )
    {
        _logger = loggerFactory.CreateLogger<ContractsController>();
        _containerService = containerService;
        _conversationService = conversationService;
        _userService = userService;
    }

    [HttpGet("{containerId}")]
    public async Task<ActionResult<ContainerDto?>> GetContainer([NotEmptyGuid] Guid containerId)
    {
        var container = await _containerService.GetContainer(containerId);
        return container.ToDtoOrNull();
    }

    [HttpGet]
    public async Task<ActionResult<List<ContainerDto>>> GetContainers()
    {
        var contracts = await _containerService.GetContainers();
        return contracts.ToDto();
    }

    [HttpGet("{containerId}/conversations")]
    public async Task<ActionResult<List<GetConversationForContainerDto>>> GetConversations([NotEmptyGuid] Guid containerId)
    {
        var container = await _containerService.GetContainer(containerId) ??
                        throw new NotFoundException($"Container with id {containerId} not found");

        var tagNos = container.Tags.Select(t => t.TagNo).ToList();
        var conversations = await _conversationService.GetConversationsForTagNos(tagNos);

        var userIds = conversations.SelectMany(conversation =>
            conversation.Participants.Select(p => p.UserId)).ToList();
        var userIdNameMap = await _userService.GetDisplayNames(userIds);

        return conversations.Select(conversation => conversation.ToDtoWithTagNo(userIdNameMap)).ToList();
    }
}
