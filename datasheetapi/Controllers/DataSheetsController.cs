using datasheetapi.Service;

namespace datasheetapi.Controllers;

[ApiController]
[Route("[controller]")]
public class DataSheetsController : ControllerBase
{
    private readonly IDataSheetService _dataSheetService;

    public DataSheetsController(IDataSheetService dataSheetService)
    {
        _dataSheetService = dataSheetService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var dataSheet = await _dataSheetService.GetDataSheetById(id);

            if (dataSheet == null)
            {
                return NotFound();
            }

            return Ok(dataSheet);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var dataSheets = await _dataSheetService.GetAllDatasheets();

            if (!dataSheets.Any())
            {
                return NotFound();
            }

            return Ok(dataSheets);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("contractor/{id:guid}", Name = "GetDatasheetsForContractor")]
    public async Task<ActionResult<List<DataSheetDto>>> GetDatasheetsForContractor([FromQuery] Guid id)
    {
        return await _dataSheetService.GetDatasheetsForContractor(id);
    }
}
