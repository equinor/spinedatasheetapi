using datasheetapi.Services;

namespace datasheetapi.Controllers;

[ApiController]
[Route("datasheets")]
public class DataSheetsController : ControllerBase
{
    private readonly IDatasheetService _dataSheetService;

    public DataSheetsController(IDatasheetService dataSheetService)
    {
        _dataSheetService = dataSheetService;
    }

    [HttpGet("{id:guid}", Name = "GetDatasheet")]
    public async Task<ActionResult<DatasheetDto>> GetById(Guid id)
    {
        try
        {
            var dataSheet = await _dataSheetService.GetDatasheetById(id);

            if (dataSheet == null)
            {
                return NotFound();
            }

            return Ok(dataSheet);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(Name = "GetDatasheets")]
    public async Task<ActionResult<List<DatasheetDto>>> GetAll()
    {
        try
        {
            var dataSheets = await _dataSheetService.GetAllDatasheets();

            return Ok(dataSheets);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("project/{id:guid}", Name = "GetDatasheetsForProject")]
    public async Task<ActionResult<List<DatasheetDto>>> GetDatasheetsForProject([FromQuery] Guid id)
    {
        try
        {
            var dataSheets = await _dataSheetService.GetDatasheetsForProject(id);

            return Ok(dataSheets);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
