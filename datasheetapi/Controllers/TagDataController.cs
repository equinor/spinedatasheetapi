using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;

namespace datasheetapi.Controllers;

[ApiController]
[Route("tagdata")]
public class TagDataController : ControllerBase
{
    private readonly ILogger<TagDataController> _logger;
    private readonly ITagDataService _tagDataService;

    public TagDataController(
        ILoggerFactory loggerFactory,
        ITagDataService tagDataService
        )
    {
        _logger = loggerFactory.CreateLogger<TagDataController>();
        _tagDataService = tagDataService;
    }

    [HttpGet("{tagNo}")]
    public async Task<ActionResult<ITagDataDto>> GetTagDataByTagNo([Required] string tagNo)
    {
        var tagData = await _tagDataService.GetTagDataByTagNo(tagNo);

        var tagDataDto = tagData.ToDto();

        return Ok(tagDataDto);
    }

    [HttpGet]
    public async Task<ActionResult<List<ITagDataDto>>> GetAllTagData()
    {
        var allTagData = await _tagDataService.GetAllTagData();
        var tagDataDtos = allTagData.ToDto();

        return Ok(tagDataDtos);
    }
}
