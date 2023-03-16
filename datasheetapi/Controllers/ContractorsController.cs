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
    public Contractor GetContractor([FromQuery] Guid id)
    {
        return _contractorService.GetContractor(id);
    }

    [HttpGet(Name = "GetContractors")]
    public List<Contractor> GetContractors()
    {
        return _contractorService.GetContractors();
    }

    [HttpGet("contractor/{id}", Name = "GetContractorsForProject")]
    public List<Contractor> GetContractorsForProject([FromQuery] Guid id)
    {
        return _contractorService.GetContractorsForProject(id);
    }
}
