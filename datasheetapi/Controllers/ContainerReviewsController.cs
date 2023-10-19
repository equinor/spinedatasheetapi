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
    private readonly IContainerReviewService _containerReviewService;

    public ContainerReviewsController(ILoggerFactory loggerFactory, IContainerReviewService reviewService)
    {
        _logger = loggerFactory.CreateLogger<ContainerReviewsController>();
        _containerReviewService = reviewService;
    }

    [HttpGet("{containerReviewId}")]
    public async Task<ActionResult<ContainerReviewDto>> GetContainerReview([NotEmptyGuid] Guid containerReviewId)
    {
        var review = await _containerReviewService.GetContainerReview(containerReviewId);
        return Ok(review);
    }

    [HttpGet]
    public async Task<ActionResult<List<ContainerReviewDto>>> GetContainerReviews([FromQuery] Guid containerId)
    {
        // TODO: Add filtering on containerId
        var reviews = await _containerReviewService.GetContainerReviews();
        return reviews.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult<ContainerReviewDto>> CreateContainerReview(
        [FromBody][Required] CreateContainerReviewDto review)
    {
        var existingReview = await _containerReviewService.GetContainerReviewForContainer(
                review.RevisionContainerId);
        if (existingReview != null)
        {
            return Conflict("A review already exists for this container");
        }

        var savedReview = await _containerReviewService.CreateContainerReview(
                review.ToModel(),
                Utils.GetAzureUniqueId(HttpContext.User));

        return savedReview.ToDto();
    }
}
