using api.Services;

using datasheetapi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("datasheets")]
public class DatasheetsController : ControllerBase
{
    private readonly IDatasheetService _datasheetService;

    public DatasheetsController(IDatasheetService datasheetService)
    {
        _datasheetService = datasheetService;
    }

    [HttpGet("{id}", Name = "GetDatasheet")]
    public async Task<ActionResult<Datasheet>> GetDatasheet([FromQuery] Guid id)
    {
        return await _datasheetService.GetDatasheet(id);
    }

    [HttpGet(Name = "GetDatasheets")]
    public async Task<ActionResult<List<Datasheet>>> GetDatasheets()
    {
        return await _datasheetService.GetDatasheets();
    }

    [HttpGet("contractor/{id}", Name = "GetDatasheetsForContractor")]
    public async Task<ActionResult<List<Datasheet>>> GetDatasheetsForContractor([FromQuery] Guid id)
    {
        return await _datasheetService.GetDatasheetsForContractor(id);
    }
}
