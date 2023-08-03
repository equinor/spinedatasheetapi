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

    [HttpGet("{id}", Name = "GetContract")]
    public async Task<ActionResult<ContractDto?>> GetContract([FromQuery] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest();
        }

        try
        {
            var contract = await _contractService.GetContract(id);
            if (contract == null)
            {
                return NotFound();
            }
            return contract.ToDtoOrNull();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting contract with id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(Name = "GetContracts")]
    public async Task<ActionResult<List<ContractDto>>> GetContracts()
    {
        try
        {
            var contracts = await _contractService.GetContracts();
            return contracts.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all contracts");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("contractor/{id}", Name = "GetContractsForContractor")]
    public async Task<ActionResult<List<ContractDto>>> GetContractsForContractor([FromQuery] Guid id)
    {
        try
        {
            var contracts = await _contractService.GetContractsForContractor(id);
            return contracts.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting contracts for contractor with id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
