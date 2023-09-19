using datasheetapi.Adapters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("usertags")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class UserTagsController : ControllerBase
{
    private readonly ILogger<UserTagsController> _logger;
    private readonly IUserTagService _userTagService;

    public UserTagsController(ILoggerFactory loggerFactory, IUserTagService userTagService)
    {
        _logger = loggerFactory.CreateLogger<UserTagsController>();
        _userTagService = userTagService;
    }

    [HttpGet("{fusionContextId}", Name = "GetUsersForProject")]
    public async Task<ActionResult<List<UserTagDto>?>> GetUsersForProject(string fusionContextId, [FromQuery] string search = "", [FromQuery] int top = 20, [FromQuery] int skip = 0)
    {
        var fusionRepsonse = await _userTagService.GetUsersFromOrgChart(fusionContextId, search, top, skip);
        return Ok(fusionRepsonse);
    }
}
