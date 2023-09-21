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
    private readonly IFusionPeopleService _fusionPeopleService;

    public UserTagsController(ILoggerFactory loggerFactory, IFusionPeopleService fusionPeopleService)
    {
        _logger = loggerFactory.CreateLogger<UserTagsController>();
        _fusionPeopleService = fusionPeopleService;
    }

    [HttpGet("{fusionContextId}", Name = "GetUsersForProject")]
    public async Task<ActionResult<List<UserTagDto>?>> GetUsersForProject(string fusionContextId, [FromQuery] string? search, [FromQuery] int top = 20, [FromQuery] int skip = 0)
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
