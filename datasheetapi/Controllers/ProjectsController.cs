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
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(ILoggerFactory loggerFactory, IProjectService projectService)
    {
        _logger = loggerFactory.CreateLogger<ProjectsController>();
        _projectService = projectService;
    }

    [HttpGet("{id}", Name = "GetProject")]
    public async Task<ActionResult<Project>> GetProject([FromQuery] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest();
        }

        try
        {
            var project = await _projectService.GetProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting project with id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
