using datasheetapi.Adapters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("tagusers")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class TagUsersController : ControllerBase
{
    private readonly ILogger<TagUsersController> _logger;

    public TagUsersController(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TagUsersController>();
    }

    public async Task<IActionResult> GetUsersForProject(string? search, int top, int skip)
    {
        return Ok(await _userResponsibilityService.GetFusionUserResponsibilitesOnProject(contract, search ?? "", top, skip));
    }
}
