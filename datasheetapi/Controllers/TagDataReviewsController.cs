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
public class TagDataReviewsController : ControllerBase
{
    private readonly TagDataReviewService _reviewService;

    public TagDataReviewsController(TagDataReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("{id}", Name = "GetReview")]
    public async Task<ActionResult<TagDataReview?>> GetReview([FromQuery] Guid id)
    {
        return await _reviewService.GetTagDataReview(id);
    }

    [HttpGet(Name = "GetReviews")]
    public async Task<ActionResult<List<TagDataReview>>> GetReviews()
    {
        return await _reviewService.GetTagDataReviews();
    }

    [HttpGet("tag/{id}", Name = "GetReviewsForTag")]
    public async Task<ActionResult<List<TagDataReview>>> GetReviewsForTag(Guid id)
    {
        return await _reviewService.GetReviewsForTag(id);
    }

    [HttpGet("project/{id}", Name = "GetReviewsForProject")]
    public async Task<ActionResult<List<TagDataReview>>> GetReviewsForProject([FromQuery] Guid id)
    {
        return await _reviewService.GetTagDataReviewsForProject(id);
    }

    [HttpPost(Name = "CreateReview")]
    public async Task<ActionResult<TagDataReview>> CreateReview([FromBody] TagDataReview review)
    {
        var httpContext = HttpContext;
        var user = httpContext.User;
        var fusionIdentity = user.Identities.FirstOrDefault(i => i is Fusion.Integration.Authentication.FusionIdentity) as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ?? throw new Exception("Could not get Azure Unique Id");
        return await _reviewService.CreateTagDataReview(review, azureUniqueId);
    }
}
