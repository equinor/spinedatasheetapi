namespace datasheetapi.Controllers;

[ApiController]
[Route("tagdata")]
public class TagDataController : ControllerBase
{
    private readonly ITagDataService _tagDataService;

    public TagDataController(ITagDataService tagDataService)
    {
        _tagDataService = tagDataService;
    }

    [HttpGet("{id:guid}", Name = "GetTagDataById")]
    public async Task<ActionResult> GetTagDataById(Guid id)
    {
        try
        {
            var tagData = await _tagDataService.GetTagDataDtoById(id);

            if (tagData == null)
            {
                return NotFound();
            }

            return Ok(tagData);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(Name = "GetAllTagData")]
    public async Task<ActionResult<List<object>>> GetAllTagData()
    {
        try
        {
            var tagData = await _tagDataService.GetAllTagDataDtos();

            var tagDataDtos = tagData.Cast<object>().ToList();

            return Ok(tagDataDtos);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("project/{id:guid}", Name = "GetTagDataForProject")]
    public async Task<ActionResult> GetTagDataForProject([FromQuery] Guid id)
    {
        try
        {
            var tagData = await _tagDataService.GetTagDataDtosForProject(id);

            var tagDataDtos = tagData.Cast<object>().ToList();

            return Ok(tagDataDtos);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
