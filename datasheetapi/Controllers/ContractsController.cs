using datasheetapi.Adapters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("contracts")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;
    private readonly ILogger<ContractsController> _logger;

    public ContractsController(ILoggerFactory loggerFactory, IContractService contractService)
    {
        _logger = loggerFactory.CreateLogger<ContractsController>();
        _contractService = contractService;
    }

    [HttpGet("{contractId}", Name = "GetContract")]
    public async Task<ActionResult<ContractDto?>> GetContract([NotEmptyGuid] Guid contractId)
    {
        var contract = await _contractService.GetContract(contractId);
        return contract.ToDtoOrNull();
    }

    [HttpGet(Name = "GetContracts")]
    public async Task<ActionResult<List<ContractDto>>> GetContracts()
    {
        var contracts = await _contractService.GetContracts();
        return contracts.ToDto();
    }

}
