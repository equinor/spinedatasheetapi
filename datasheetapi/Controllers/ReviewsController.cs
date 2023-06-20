using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("reviews")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class ReviewsController : ControllerBase
{
    private readonly ReviewService _reviewService;

    public ReviewsController(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("{id}", Name = "GetReview")]
    public async Task<ActionResult<Review?>> GetReview([FromQuery] Guid id)
    {
        return await _reviewService.GetReview(id);
    }

    [HttpGet(Name = "GetReviews")]
    public async Task<ActionResult<List<Review>>> GetReviews()
    {
        return await _reviewService.GetReviews();
    }

    [HttpGet("tag/{id}", Name = "GetReviewsForTag")]
    public async Task<ActionResult<List<Review>>> GetReviewsForTag(Guid id)
    {
        return await _reviewService.GetReviewsForTag(id);
    }

    [HttpGet("project/{id}", Name = "GetReviewsForProject")]
    public async Task<ActionResult<List<Review>>> GetReviewsForProject([FromQuery] Guid id)
    {
        return await _reviewService.GetReviewsForProject(id);
    }

    [HttpPost(Name = "CreateReview")]
    public async Task<ActionResult<Review>> CreateReview([FromBody] Review review)
    {
        var httpContext = HttpContext;
        var user = httpContext.User;
        var fusionIdentity = user.Identities.FirstOrDefault(i => i is Fusion.Integration.Authentication.FusionIdentity) as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ?? throw new Exception("Could not get Azure Unique Id");
        return await _reviewService.CreateReview(review, azureUniqueId);
    }
}
