using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;

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
    private readonly IReviewerService _reviewerTagDataReviewService;

    public TagDataReviewsController(ILoggerFactory loggerFactory,
        ITagDataReviewService reviewService, IReviewerService reviewerTagDataReviewService)
    {
        _logger = loggerFactory.CreateLogger<TagDataReviewsController>();
        _reviewService = reviewService;
        _reviewerTagDataReviewService = reviewerTagDataReviewService;
    }

    [HttpGet("{reviewId}", Name = "GetReviewById")]
    public async Task<ActionResult<TagDataReviewDto?>> GetReview([NotEmptyGuid] Guid reviewId)
    {
        var review = await _reviewService.GetTagDataReview(reviewId);
        return review.ToDtoOrNull();
    }

    [HttpGet(Name = "GetReviews")]
    public async Task<ActionResult<List<TagDataReviewDto>>> GetReviews()
    {
        var reviews = await _reviewService.GetTagDataReviews();
        return reviews.ToDto();
    }

    [HttpPost(Name = "CreateReview")]
    public async Task<ActionResult<TagDataReviewDto?>> CreateReview(
        [FromBody][Required] CreateTagDataReviewDto reviewDto)
    {
        var result = await _reviewService.CreateTagDataReview(
            reviewDto.ToModel(), Utils.GetAzureUniqueId(HttpContext.User));
        return result.ToDtoOrNull();
    }

    [HttpPost("{reviewId}/reviewertagdatareviews", Name = "CreateReviewerTagDataReview")]
    public async Task<ActionResult<ReviewerTagDataReviewDto?>> CreateReviewerTagDataReview(
        [NotEmptyGuid] Guid reviewId,
        [Required] List<CreateReviewerTagDataReviewDto> reviewDtos)
    {
        var result = await _reviewerTagDataReviewService.CreateReviewers(
            reviewId, reviewDtos.ToModel());

        return result.ToDtoOrNull();
    }
}
