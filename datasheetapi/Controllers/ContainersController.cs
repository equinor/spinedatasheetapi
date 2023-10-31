using datasheetapi.Adapters;

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
    private readonly ILogger<ContractsController> _logger;

    public ContainersController(ILoggerFactory loggerFactory, IContainerService containerService)
    {
        _logger = loggerFactory.CreateLogger<ContractsController>();
        _containerService = containerService;
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

}
