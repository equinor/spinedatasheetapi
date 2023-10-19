using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("container-reviewers")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class ContainerReviewersController : ControllerBase
{
    private readonly ILogger<ContainerReviewersController> _logger;
    private readonly IReviewerService _reviewerService;
    private readonly IUserService _userService;

    public ContainerReviewersController(
        ILoggerFactory loggerFactory,
        IReviewerService reviewerService,
        IUserService userService
        )
    {
        _logger = loggerFactory.CreateLogger<ContainerReviewersController>();
        _reviewerService = reviewerService;
        _userService = userService;
    }

    [HttpGet("{containerReviewerId}")]
    public async Task<ActionResult<ContainerReviewDto>> GetContainerReviewer(Guid containerReviewerId)
    {

    }

    [HttpPost]
    public async Task<ActionResult<List<ReviewerDto>?>> CreateContainerReviewers(
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

    [HttpPut("{reviewerId}")]
    public async Task<ActionResult<ReviewerDto?>> UpdateContainerReviewer(
        [NotEmptyGuid] Guid reviewerId,
        [Required] UpdateReviewerDto updateReviewerDto)
    {
        var reviewStatus = updateReviewerDto.ReviewStatus.MapReviewStatusDtoToModel();
        var result = await _reviewerService.UpdateReviewer(reviewerId, Utils.GetAzureUniqueId(HttpContext.User), reviewStatus);

        var displayName = await _userService.GetDisplayName(result.UserId);

        return result.ToDto(displayName);
    }
}
