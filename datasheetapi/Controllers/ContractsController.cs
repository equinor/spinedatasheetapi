using api.Services;

using datasheetapi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("contracts")]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
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
