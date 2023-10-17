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
    private readonly IUserService _userService;

    public TagDataReviewsController(
        ILoggerFactory loggerFactory,
        ITagDataReviewService reviewService,
        IReviewerService reviewerService,
        IUserService userService
        )
    {
        _logger = loggerFactory.CreateLogger<TagDataReviewsController>();
        _reviewService = reviewService;
        _reviewerService = reviewerService;
        _userService = userService;
    }

    [HttpGet("{reviewId}", Name = "GetReviewById")]
    public async Task<ActionResult<TagDataReviewDto?>> GetReview([NotEmptyGuid] Guid reviewId)
    {
        var result = await _reviewService.GetTagDataReview(reviewId);

        var reviewerIds = result.Reviewers.Select(r => r.ReviewerId).ToList();

        var displayNameMap = await _userService.GetDisplayNames(reviewerIds);

        return result.ToDto(displayNameMap);
    }

    [HttpGet(Name = "GetReviews")]
    public async Task<ActionResult<List<TagDataReviewDto>>> GetReviews(Guid? reviewerId)
    {
        var reviews = await _reviewService.GetTagDataReviews(reviewerId);

        var userIds = reviews.SelectMany(tagReview =>
                        tagReview.Reviewers.Select(p => p.ReviewerId)).ToList();
        var userIdNameMap = await _userService.GetDisplayNames(userIds);

        return reviews.Select(review => review.ToDto(userIdNameMap)).ToList();
    }

    [HttpPost(Name = "CreateReview")]
    public async Task<ActionResult<TagDataReviewDto?>> CreateReview(
        [FromBody][Required] CreateTagDataReviewDto reviewDto)
    {
        var result = await _reviewService.CreateTagDataReview(
            reviewDto.ToModel(), Utils.GetAzureUniqueId(HttpContext.User));

        var reviewerIds = result.Reviewers.Select(r => r.ReviewerId).ToList();

        var displayNameMap = await _userService.GetDisplayNames(reviewerIds);

        return result.ToDto(displayNameMap);
    }

    [HttpPost("{reviewId}/reviewers", Name = "CreateReviewers")]
    public async Task<ActionResult<List<ReviewerDto>?>> CreateReviewers(
        [NotEmptyGuid] Guid reviewId,
        [Required] List<CreateReviewerDto> reviewDtos)
    {
        var result = await _reviewerService.CreateReviewers(
            reviewId, reviewDtos.ToModel());

        var userIds = result.Select(tagReview =>
                tagReview.ReviewerId).ToList();

        var userIdNameMap = await _userService.GetDisplayNames(userIds);

        return result.ToDto(userIdNameMap);
    }

    [HttpPut("{reviewId}/reviewers/{reviewerId}", Name = "UpdateReview")]
    public async Task<ActionResult<ReviewerDto?>> UpdateReview(
    [NotEmptyGuid] Guid reviewId,
    [NotEmptyGuid] Guid reviewerId,
    [Required] UpdateReviewerDto updateReviewerDto)
    {
        var reviewStatus = updateReviewerDto.ReviewStatus.MapReviewStatusDtoToModel();
        var result = await _reviewerService.UpdateReviewer(reviewId, reviewerId, Utils.GetAzureUniqueId(HttpContext.User), reviewStatus);

        var displayName = await _userService.GetDisplayName(result.ReviewerId);

        return result.ToDto(displayName);
    }
}
