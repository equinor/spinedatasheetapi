using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("container-reviews/{containerReviewId}/container-reviewers/{containerReviewerId}/tag-reviewers")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class TagReviewersController : ControllerBase
{
    private readonly ILogger<TagReviewersController> _logger;
    private readonly IReviewerService _reviewerService;
    private readonly IUserService _userService;

    public TagReviewersController(
        ILoggerFactory loggerFactory,
        IReviewerService reviewerService,
        IUserService userService
        )
    {
        _logger = loggerFactory.CreateLogger<TagReviewersController>();
        _reviewerService = reviewerService;
        _userService = userService;
    }

    [HttpPost("", Name = "CreateReviewers")]
    public async Task<ActionResult<List<ReviewerDto>?>> CreateReviewers(
        [NotEmptyGuid] Guid reviewId,
        [Required] List<CreateReviewerDto> reviewDtos)
    {
        var result = await _reviewerService.CreateReviewers(
            reviewId, reviewDtos.ToModel());

        var userIds = result.Select(tagReview =>
                tagReview.UserId).ToList();

        var userIdNameMap = await _userService.GetDisplayNames(userIds);

        return result.ToDto(userIdNameMap);
    }

    [HttpPut("{reviewerId}", Name = "UpdateReview")]
    public async Task<ActionResult<ReviewerDto?>> UpdateReview(
        [NotEmptyGuid] Guid reviewerId,
        [Required] UpdateReviewerDto updateReviewerDto)
    {
        var reviewStatus = updateReviewerDto.ReviewStatus.MapReviewStatusDtoToModel();
        var result = await _reviewerService.UpdateReviewer(reviewerId, Utils.GetAzureUniqueId(HttpContext.User), reviewStatus);

        var displayName = await _userService.GetDisplayName(result.UserId);

        return result.ToDto(displayName);
    }
}
