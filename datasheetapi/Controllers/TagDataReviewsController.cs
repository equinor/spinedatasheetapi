using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using datasheetapi.Adapters;

namespace datasheetapi.Controllers;

[ApiController]
[Route("tagdatareviews")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class TagDataReviewsController : ControllerBase
{
    private readonly ILogger<TagDataReviewsController> _logger;
    private readonly ITagDataReviewService _reviewService;

    public TagDataReviewsController(ILoggerFactory loggerFactory, ITagDataReviewService reviewService)
    {
        _logger = loggerFactory.CreateLogger<TagDataReviewsController>();
        _reviewService = reviewService;
    }

    [HttpGet("{id}", Name = "GetReview")]
    public async Task<ActionResult<TagDataReviewDto?>> GetReview([FromQuery] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest();
        }

        try
        {
            var review = await _reviewService.GetTagDataReview(id);
            if (review == null)
            {
                return NotFound();
            }
            return review.ToDtoOrNull();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tag data review for id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(Name = "GetReviews")]
    public async Task<ActionResult<List<TagDataReviewDto>>> GetReviews()
    {
        try
        {
            var reviews = await _reviewService.GetTagDataReviews();
            return reviews.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all tag data reviews");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("tag/{id}", Name = "GetReviewsForTag")]
    public async Task<ActionResult<List<TagDataReviewDto>>> GetReviewsForTag(Guid id)
    {
        try
        {
            var reviews = await _reviewService.GetTagDataReviewsForTag(id);
            return reviews.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tag data reviews for tag id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("project/{id}", Name = "GetReviewsForProject")]
    public async Task<ActionResult<List<TagDataReviewDto>>> GetReviewsForProject([FromQuery] Guid id)
    {
        try
        {
            var reviews = await _reviewService.GetTagDataReviewsForProject(id);
            return reviews.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tag data reviews for project id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(Name = "CreateReview")]
    public async Task<ActionResult<TagDataReviewDto?>> CreateReview([FromBody] TagDataReviewDto reviewDto)
    {
        var httpContext = HttpContext;
        var user = httpContext.User;
        var fusionIdentity = user.Identities.FirstOrDefault(i => i is Fusion.Integration.Authentication.FusionIdentity) as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ?? throw new Exception("Could not get Azure Unique Id");

        if (reviewDto == null) { return BadRequest(); }

        var review = reviewDto.ToModelOrNull();
        if (review == null) { return BadRequest(); }

        try
        {
            var result = await _reviewService.CreateTagDataReview(review, azureUniqueId);
            return result.ToDtoOrNull();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating tag data review");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
