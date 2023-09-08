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

    [HttpGet("{projectId}", Name = "GetUsersForProject")]
    public async Task<IActionResult> GetUsersForProject(string projectId, [FromQuery] string? search, [FromQuery] int top = 20, [FromQuery] int skip = 0)
    {
        var fusionRepsonse = await _fusionPeopleService.GetAllPersonsOnProject(projectId, search ?? "", top, skip);
        Console.WriteLine(fusionRepsonse);
        return Ok(fusionRepsonse);
    }
}
