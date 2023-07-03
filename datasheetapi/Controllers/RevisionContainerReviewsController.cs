using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("revisionreviews")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class RevisionContainerReviewsController : ControllerBase
{
    private readonly RevisionContainerReviewService _reviewService;

    public RevisionContainerReviewsController(RevisionContainerReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("{id}", Name = "GetRevisionReview")]
    public async Task<ActionResult<RevisionContainerReview?>> GetReview([FromQuery] Guid id)
    {
        return await _reviewService.GetTagDataReview(id);
    }

    [HttpGet(Name = "GetRevisionReviews")]
    public async Task<ActionResult<List<RevisionContainerReview>>> GetReviews()
    {
        return await _reviewService.GetTagDataReviews();
    }

    [HttpGet("tag/{id}", Name = "GetRevisionReviewsForTag")]
    public async Task<ActionResult<List<RevisionContainerReview>>> GetReviewsForTag(Guid id)
    {
        return await _reviewService.GetReviewsForTag(id);
    }

    [HttpGet("project/{id}", Name = "GetRevisionReviewsForProject")]
    public async Task<ActionResult<List<RevisionContainerReview>>> GetReviewsForProject([FromQuery] Guid id)
    {
        return await _reviewService.GetTagDataReviewsForProject(id);
    }

    [HttpPost(Name = "CreateRevisionReview")]
    public async Task<ActionResult<RevisionContainerReview>> CreateReview([FromBody] RevisionContainerReview review)
    {
        var httpContext = HttpContext;
        var user = httpContext.User;
        var fusionIdentity = user.Identities.FirstOrDefault(i => i is Fusion.Integration.Authentication.FusionIdentity) as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ?? throw new Exception("Could not get Azure Unique Id");
        return await _reviewService.CreateTagDataReview(review, azureUniqueId);
    }
}
