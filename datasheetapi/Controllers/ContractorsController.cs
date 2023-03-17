using api.Services;

using datasheetapi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("contractors")]
public class ContractorsController : ControllerBase
{
    private readonly IContractorService _contractorService;

    public ContractorsController(IContractorService contractorService)
    {
        _contractorService = contractorService;
    }

    [HttpGet("{id}", Name = "GetContractor")]
    public async Task<ActionResult<Contractor>> GetContractor([FromQuery] Guid id)
    {
        return await _contractorService.GetContractor(id);
    }

    [HttpGet(Name = "GetContractors")]
    public async Task<ActionResult<List<Contractor>>> GetContractors()
    {
        return await _contractorService.GetContractors();
    }

    [HttpGet("contractor/{id}", Name = "GetContractorsForProject")]
    public async Task<ActionResult<List<Contractor>>> GetContractorsForProject([FromQuery] Guid id)
    {
        return await _contractorService.GetContractorsForProject(id);
    }
}
