using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("reviews")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class ReviewsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ReviewsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpGet("{id}", Name = "GetContract")]
    public async Task<ActionResult<Contract>> GetDatasheet([FromQuery] Guid id)
    {
        return await _contractService.GetContract(id);
    }

    [HttpGet(Name = "GetContracts")]
    public async Task<ActionResult<List<Contract>>> GetDatasheets()
    {
        return await _contractService.GetContracts();
    }

    [HttpGet("contractor/{id}", Name = "GetContractsForContractor")]
    public async Task<ActionResult<List<Contract>>> GetDatasheetsForContractor([FromQuery] Guid id)
    {
        return await _contractService.GetContractsForContractor(id);
    }
}
