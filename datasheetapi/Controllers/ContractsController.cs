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
    public Contract GetDatasheet([FromQuery] Guid id)
    {
        return _contractService.GetContract(id);
    }

    [HttpGet(Name = "GetContracts")]
    public List<Contract> GetDatasheets()
    {
        return _contractService.GetContracts();
    }

    [HttpGet("contractor/{id}", Name = "GetContractsForContractor")]
    public List<Contract> GetDatasheetsForContractor([FromQuery] Guid id)
    {
        return _contractService.GetContractsForContractor(id);
    }
}
