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
    private readonly FusionPeopleService _fusionPeopleService;

    public TagUsersController(ILoggerFactory loggerFactory, FusionPeopleService fusionPeopleService)
    {
        _logger = loggerFactory.CreateLogger<TagUsersController>();
        _fusionPeopleService = fusionPeopleService;
    }

    [HttpGet("{orgChartId}", Name = "GetUsersForProject")]
    public async Task<IActionResult> GetUsersForProject(string orgChartId, [FromQuery] string? search, [FromQuery] int top = 20, [FromQuery] int skip = 0)
    {
        var claims = HttpContext.User.Claims;
        var orgChart = claims.Where(x => x.Type == "http://schemas.fusion.equinor.com/identity/claims/orgprojectid");
        Console.WriteLine("OrgChart: " + orgChart?.Count());

        var fusionRepsonse = await _fusionPeopleService.GetAllPersonsOnProject(orgChartId, search ?? "", top, skip);
        Console.WriteLine(fusionRepsonse);
        return Ok(fusionRepsonse);
    }
}
