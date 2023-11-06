using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;
using datasheetapi.Dtos.ContainerReviewer;
using datasheetapi.Dtos.TagReviewer;

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
    public async Task<ActionResult<List<ContainerReviewerDto>>> GetContainerReviewers([FromQuery] Guid userId)
    {
        var result = await _containerReviewerService.GetContainerReviewers(userId);

        // TODO: Implement this properly
        var userIdNameMap = await _userService.GetDisplayNames(new List<Guid> { userId });

        var dto = result.ToDto(userIdNameMap);

        return Ok(dto);
    }

    [HttpGet("{containerReviewerId}")]
    public async Task<ActionResult<ContainerReviewerDto>> GetContainerReviewer(Guid containerReviewerId)
    {
        var result = await _containerReviewerService.GetContainerReviewer(containerReviewerId);

        var userIdNameMap = await _userService.GetDisplayNames(new List<Guid> { result.UserId });

        var dto = result.ToDto(userIdNameMap);

        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<List<ContainerReviewerDto>>> GetContainerReviewersForContainerReview(
        Guid containerReviewId, [FromQuery] Guid userId)
    {
        var result = await _containerReviewerService.GetContainerReviewersForContainerReview(containerReviewId, userId);

        // TODO: Implement this properly
        var userIdNameMap = await _userService.GetDisplayNames(new List<Guid> { userId });

        var dto = result.ToList().ToDto(userIdNameMap);

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ContainerReviewerDto>> CreateContainerReviewer(Guid containerReviewId,
        [Required] CreateContainerReviewerDto reviewDto)
    {
        var containerReviewerModel = reviewDto.ToModel();

        var result = await _containerReviewerService.CreateContainerReviewer(containerReviewId, containerReviewerModel);

        var userIdNameMap = await _userService.GetDisplayNames(new List<Guid> { result.UserId });

        return Ok(result.ToDto(userIdNameMap));
    }

    [HttpPut("{containerReviewerId}")]
    public async Task<ActionResult<ContainerReviewerDto?>> UpdateContainerReviewer(
        [NotEmptyGuid] Guid containerReviewerId,
        [Required] UpdateContainerReviewerDto updateTagReviewerDto)
    {
        var reviewState = ContainerReviewerAdapter.MapContainerReviewStateDtoToModel(updateTagReviewerDto.State);
        var result = await _containerReviewerService.UpdateContainerReviewer(containerReviewerId,
            Utils.GetAzureUniqueId(HttpContext.User), reviewState);

        var userIdNameMap = await _userService.GetDisplayNames(new List<Guid> { result.UserId });

        return result.ToDto(userIdNameMap);
    }
}
