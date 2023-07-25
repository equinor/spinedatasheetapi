using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

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
    public async Task<ActionResult<TagDataReview?>> GetReview([FromQuery] Guid id)
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
            return Ok(review);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tag data review for id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(Name = "GetReviews")]
    public async Task<ActionResult<List<TagDataReview>>> GetReviews()
    {
        try
        {
            return await _reviewService.GetTagDataReviews();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all tag data reviews");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("tag/{id}", Name = "GetReviewsForTag")]
    public async Task<ActionResult<List<TagDataReview>>> GetReviewsForTag(Guid id)
    {
        try
        {
            return await _reviewService.GetReviewsForTag(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tag data reviews for tag id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("project/{id}", Name = "GetReviewsForProject")]
    public async Task<ActionResult<List<TagDataReview>>> GetReviewsForProject([FromQuery] Guid id)
    {
        try
        {
            return await _reviewService.GetTagDataReviewsForProject(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tag data reviews for project id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(Name = "CreateReview")]
    public async Task<ActionResult<TagDataReview>> CreateReview([FromBody] TagDataReview review)
    {
        var httpContext = HttpContext;
        var user = httpContext.User;
        var fusionIdentity = user.Identities.FirstOrDefault(i => i is Fusion.Integration.Authentication.FusionIdentity) as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ?? throw new Exception("Could not get Azure Unique Id");

        if (review == null) { return BadRequest(); }

        try
        {
            return await _reviewService.CreateTagDataReview(review, azureUniqueId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating tag data review");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
