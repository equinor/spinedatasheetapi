namespace datasheetapi.Controllers;

[ApiController]
[Route("datasheets")]
public class DataSheetsController : ControllerBase
{
    private readonly ITagDataService _tagDataService;

    public DataSheetsController(ITagDataService tagDataService)
    {
        _tagDataService = tagDataService;
    }

    [HttpGet("{id:guid}", Name = "GetDatasheet")]
    public async Task<ActionResult<TagDataDto>> GetById(Guid id)
    {
        try
        {
            var dataSheet = await _tagDataService.GetDatasheetById(id);

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
    public async Task<ActionResult<List<TagDataDto>>> GetAll()
    {
        try
        {
            var dataSheets = await _tagDataService.GetAllDatasheets();

            return Ok(dataSheets);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("project/{id:guid}", Name = "GetDatasheetsForProject")]
    public async Task<ActionResult<List<TagDataDto>>> GetDatasheetsForProject([FromQuery] Guid id)
    {
        try
        {
            var dataSheets = await _tagDataService.GetDatasheetsForProject(id);

            return Ok(dataSheets);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
