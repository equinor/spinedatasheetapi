using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;

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
    private readonly ILogger<RevisionContainerReviewsController> _logger;
    private readonly IRevisionContainerReviewService _reviewService;

    public RevisionContainerReviewsController(ILoggerFactory loggerFactory, IRevisionContainerReviewService reviewService)
    {
        _logger = loggerFactory.CreateLogger<RevisionContainerReviewsController>();
        _reviewService = reviewService;
    }

    [HttpGet("{reviewId}", Name = "GetRevisionReview")]
    public async Task<ActionResult<RevisionContainerReviewDto>> GetRevisionReview([NotEmptyGuid] Guid reviewId)
    {
        var review = await _reviewService.GetRevisionContainerReview(reviewId);
        return Ok(review);
    }

    [HttpGet(Name = "GetRevisionReviews")]
    public async Task<ActionResult<List<RevisionContainerReviewDto>>> GetRevisionReviews()
    {
        var reviews = await _reviewService.GetRevisionContainerReviews();
        return reviews.ToDto();
    }

    [HttpPost(Name = "CreateRevisionReview")]
    public async Task<ActionResult<RevisionContainerReviewDto>> CreateRevisionReview(
        [FromBody][Required] CreateContainerReviewDto review)
    {
        var existingReview = await _reviewService.GetRevisionContainerReviewForContainer(
                review.RevisionContainerId);
        if (existingReview != null)
        {
            return Conflict("A review already exists for this revision");
        }

        var savedReview = await _reviewService.CreateRevisionContainerReview(
                review.ToModel(), Utils.GetAzureUniqueId(HttpContext.User));
        return savedReview.ToDto();
    }
}
