using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;

namespace datasheetapi.Controllers;

[ApiController]
[Route("tagdata")]
public class TagDataController : ControllerBase
{
    private readonly ILogger<TagDataController> _logger;
    private readonly ITagDataService _tagDataService;
    private readonly ITagDataEnrichmentService _enrichTagDataService;

    public TagDataController(ILoggerFactory loggerFactory, ITagDataService tagDataService,
        ITagDataEnrichmentService enrichTagDataService)
    {
        _logger = loggerFactory.CreateLogger<TagDataController>();
        _tagDataService = tagDataService;
        _enrichTagDataService = enrichTagDataService;
    }

    [HttpGet("{tagNo}", Name = "GetTagDataByTagNo")]
    public async Task<ActionResult<ITagDataDto>> GetTagDataById([Required] string tagNo)
    {

        var tagData = await _tagDataService.GetTagDataByTagNo(tagNo);

        var tagDataDto = tagData.ToDto();

        tagDataDto = await _enrichTagDataService.AddReview(tagDataDto);
        tagDataDto = await _enrichTagDataService.AddRevisionContainerWithReview(tagDataDto);

        return Ok(tagDataDto);
    }

    [HttpGet(Name = "GetAllTagData")]
    public async Task<ActionResult<List<ITagDataDto>>> GetAllTagData()
    {

        var allTagData = await _tagDataService.GetAllTagData();
        var tagDataDtos = allTagData.ToDto();

        tagDataDtos = await _enrichTagDataService.AddReview(tagDataDtos);
        tagDataDtos = await _enrichTagDataService.AddRevisionContainerWithReview(tagDataDtos);

        return Ok(tagDataDtos);
    }
}
