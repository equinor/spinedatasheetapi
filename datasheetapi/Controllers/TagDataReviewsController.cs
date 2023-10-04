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
    private readonly IReviewerService _reviewerService;

    public TagDataReviewsController(ILoggerFactory loggerFactory,
        ITagDataReviewService reviewService, IReviewerService reviewerService)
    {
        _logger = loggerFactory.CreateLogger<TagDataReviewsController>();
        _reviewService = reviewService;
        _reviewerService = reviewerService;
    }

    [HttpGet("{reviewId}", Name = "GetReviewById")]
    public async Task<ActionResult<TagDataReviewDto?>> GetReview([NotEmptyGuid] Guid reviewId)
    {
        var review = await _reviewService.GetTagDataReview(reviewId);
        return review.ToDtoOrNull();
    }

    [HttpGet(Name = "GetReviews")]
    public async Task<ActionResult<List<TagDataReviewDto>>> GetReviews(Guid? reviewerId)
    {
        var reviews = await _reviewService.GetTagDataReviews(reviewerId);
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

    [HttpPost("{reviewId}/reviewers", Name = "CreateReviewers")]
    public async Task<ActionResult<List<ReviewerDto>?>> CreateReviewers(
        [NotEmptyGuid] Guid reviewId,
        [Required] List<CreateReviewerDto> reviewDtos)
    {
        var result = await _reviewerService.CreateReviewers(
            reviewId, reviewDtos.ToModel());

        return result.ToDto();
    }
}
