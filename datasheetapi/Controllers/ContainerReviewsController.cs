using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("container-reviews")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class ContainerReviewsController : ControllerBase
{
    private readonly ILogger<ContainerReviewsController> _logger;
    private readonly IRevisionContainerReviewService _reviewService;

    public ContainerReviewsController(ILoggerFactory loggerFactory, IRevisionContainerReviewService reviewService)
    {
        _logger = loggerFactory.CreateLogger<ContainerReviewsController>();
        _reviewService = reviewService;
    }

    [HttpGet("{containerReviewId}", Name = "GetRevisionReview")]
    public async Task<ActionResult<ContainerReviewDto>> GetRevisionReview([NotEmptyGuid] Guid reviewId)
    {
        var review = await _reviewService.GetRevisionContainerReview(reviewId);
        return Ok(review);
    }

    [HttpGet(Name = "GetRevisionReviews")]
    public async Task<ActionResult<List<ContainerReviewDto>>> GetRevisionReviews()
    {
        var reviews = await _reviewService.GetRevisionContainerReviews();
        return reviews.ToDto();
    }

    [HttpPost(Name = "CreateRevisionReview")]
    public async Task<ActionResult<ContainerReviewDto>> CreateRevisionReview(
        [FromBody][Required] CreateContainerReviewDto review)
    {
        var existingReview = await _reviewService.GetContainerReviewForContainer(
                review.RevisionContainerId);
        if (existingReview != null)
        {
            return Conflict("A review already exists for this revision");
        }

        var savedReview = await _reviewService.CreateContainerReview(
                review.ToModel(), Utils.GetAzureUniqueId(HttpContext.User));
        return savedReview.ToDto();
    }
}
