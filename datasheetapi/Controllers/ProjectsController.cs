using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi;

[ApiController]
[Route("projects")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IFusionPeopleService _fusionPeopleService;

    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(
        ILoggerFactory loggerFactory,
        IProjectService projectService,
        IFusionPeopleService fusionPeopleService
        )
    {
        _logger = loggerFactory.CreateLogger<ProjectsController>();
        _projectService = projectService;
        _fusionPeopleService = fusionPeopleService;
    }

    [HttpGet("{projectId}", Name = "GetProject")]
    public async Task<ActionResult<ProjectDto>> GetProject(
        [NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid projectId)
    {
        var project = await _projectService.GetProject(projectId);
        return project.ToDto();
    }

    [HttpGet("{fusionContextId}/users", Name = "GetUsersForProject")]
    public async Task<ActionResult<List<UserTagDto>?>> GetUsersForProject(
        [NotEmptyGuid(ErrorMessage = "The GUID must not be empty.")] Guid fusionContextId,
        [FromQuery] string? search, [FromQuery] int top = 20, [FromQuery] int skip = 0)
    {
        var fusionRepsonse = await _fusionPeopleService.GetAllPersonsOnProject(fusionContextId, search ?? "", top, skip);

        var userTagDtos = fusionRepsonse.Select(fusionPersonResultV1 => new UserTagDto
        {
            AzureUniqueId = fusionPersonResultV1.AzureUniqueId,
            DisplayName = fusionPersonResultV1.Name,
            Mail = fusionPersonResultV1.Mail,
            AccountType = fusionPersonResultV1.AccountType
        }).ToList();

        return userTagDtos;
    }
}
