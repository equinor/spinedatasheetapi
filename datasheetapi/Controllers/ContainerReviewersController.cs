using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("container-reviews/{containerReviewId}/container-reviewers")]
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
    private readonly ContainerReviewerService _containerReviewerService;
    private readonly IUserService _userService;

    public ContainerReviewersController(
        ILoggerFactory loggerFactory,
        ContainerReviewerService containerReviewerService,
        IUserService userService
        )
    {
        _logger = loggerFactory.CreateLogger<ContainerReviewersController>();
        _containerReviewerService = containerReviewerService;
        _userService = userService;
    }

    [HttpGet("/container-reviewers")]
    public Task<ActionResult<List<ContainerReviewDto>>> GetContainerReviewers([FromQuery] Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{containerReviewerId}")]
    public async Task<ActionResult<ContainerReviewDto>> GetContainerReviewer(Guid containerReviewerId)
    {
        var result = await _containerReviewerService.GetContainerReviewer(containerReviewerId);

        //var dto = result.ToDto();

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<ContainerReviewDto>>> GetContainerReviewersForContainerReview(Guid containerReviewId, [FromQuery] Guid userId)
    {
        var result = await _containerReviewerService.GetContainerReviewersForContainerReview(containerReviewId, userId);

        //var dto = result.ToDto();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<List<ReviewerDto>?>> CreateContainerReviewer(
        [NotEmptyGuid] Guid reviewId,
        [Required] CreateReviewerDto reviewDto)
    {
        var a = new ContainerReviewer();

        var result = await _containerReviewerService.CreateContainerReviewer(
            a);

        return Ok(result);
    }

    [HttpPut("{containerReviewerId}")]
    public Task<ActionResult<ReviewerDto?>> UpdateContainerReviewer(
        [NotEmptyGuid] Guid containerReviewerId,
        [Required] UpdateReviewerDto updateReviewerDto)
    {
        throw new NotImplementedException();
        //var reviewStatus = updateReviewerDto.ReviewStatus.MapReviewStatusDtoToModel();
        //var result = await _containerReviewerService.UpdateReviewer(containerReviewerId, Utils.GetAzureUniqueId(HttpContext.User), reviewStatus);

        //var displayName = await _userService.GetDisplayName(result.UserId);

        //return result.ToDto(displayName);
    }
}
