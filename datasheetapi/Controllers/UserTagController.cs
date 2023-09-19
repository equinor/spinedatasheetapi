using datasheetapi.Adapters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("usertag")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class UserTagController : ControllerBase
{
    private readonly ILogger<UserTagController> _logger;
    private readonly IUserTagService _userTagService;

    public UserTagController(ILoggerFactory loggerFactory, IUserTagService userTagService)
    {
        _logger = loggerFactory.CreateLogger<UserTagController>();
        _userTagService = userTagService;
    }

    [HttpGet("{fusionContextId}", Name = "GetUsersForProject")]
    public async Task<IActionResult> GetUsersForProject(string fusionContextId, [FromQuery] string? search, [FromQuery] int top = 20, [FromQuery] int skip = 0)
    {
        var fusionRepsonse = await _userTagService.GetUsersFromOrgChart(fusionContextId, search ?? "", top, skip);
        return Ok(fusionRepsonse);
    }
}
