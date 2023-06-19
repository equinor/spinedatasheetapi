namespace datasheetapi.Controllers;

[ApiController]
[Route("datasheets")]
public class TagDataController : ControllerBase
{
    private readonly ITagDataService _dataSheetService;

    public TagDataController(ITagDataService dataSheetService)
    {
        _dataSheetService = dataSheetService;
    }

    [HttpGet("{id:guid}", Name = "GetDatasheet")]
    public async Task<ActionResult> GetById(Guid id)
    {
        try
        {
            var dataSheet = await _dataSheetService.GetTagDataById(id);

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
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var dataSheets = await _dataSheetService.GetAllTagData();

            return Ok(dataSheets);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("project/{id:guid}", Name = "GetDatasheetsForProject")]
    public async Task<ActionResult> GetDatasheetsForProject([FromQuery] Guid id)
    {
        try
        {
            var dataSheets = await _dataSheetService.GetTagDataForProject(id);

            return Ok(dataSheets);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
