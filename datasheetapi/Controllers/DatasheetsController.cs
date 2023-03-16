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
    public Datasheet GetDatasheet([FromQuery] Guid id)
    {
        return _datasheetService.GetDatasheet(id);
    }

    [HttpGet(Name = "GetDatasheets")]
    public List<Datasheet> GetDatasheets()
    {
        return _datasheetService.GetDatasheets();
    }

    [HttpGet("contractor/{id}", Name = "GetDatasheetsForContractor")]
    public List<Datasheet> GetDatasheetsForContractor([FromQuery] Guid id)
    {
        return _datasheetService.GetDatasheetsForContractor(id);
    }
}
