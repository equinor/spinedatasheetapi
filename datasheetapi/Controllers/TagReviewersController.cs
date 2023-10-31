using System.ComponentModel.DataAnnotations;

using datasheetapi.Adapters;
using datasheetapi.Dtos.TagReviewer;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("container-reviewers/{containerReviewerId}/tag-reviewers")]
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
    private readonly ITagReviewerService _tagReviewerService;
    private readonly IUserService _userService;

    public TagReviewersController(
        ILoggerFactory loggerFactory,
        ITagReviewerService reviewerService,
        IUserService userService
        )
    {
        _logger = loggerFactory.CreateLogger<TagReviewersController>();
        _tagReviewerService = reviewerService;
        _userService = userService;
    }

    [HttpGet("/tag-reviewers")]
    public async Task<ActionResult<List<TagReviewerDto>>> GetAllTagReviewers([FromQuery] Guid userId)
    {
        var result = await _tagReviewerService.GetAllTagReviewers(userId);

        var userIds = result.Select(tagReview =>
            tagReview.UserId).ToList();

        var userIdNameMap = await _userService.GetDisplayNames(userIds);

        return result.ToDto(userIdNameMap);
    }

    [HttpGet("{tagReviewerId}")]
    public Task<ActionResult<TagReviewerDto>> GetTagReviewer(Guid tagReviewerId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Task<ActionResult<List<TagReviewerDto>>> GetTagReviewers()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<List<TagReviewerDto>?>> CreateTagReviewers(
        [NotEmptyGuid] Guid containerReviewerId,
        [Required] List<CreateTagReviewerDto> tagReviewerDtos)
    {
        var tagReviewersList = tagReviewerDtos.ToModel();

        var result = await _tagReviewerService.CreateReviewers(
            containerReviewerId, tagReviewersList);

        var userIds = result.Select(tagReview =>
                tagReview.UserId).ToList();

        var userIdNameMap = await _userService.GetDisplayNames(userIds);

        return result.ToDto(userIdNameMap);
    }

    [HttpPut("{tagReviewerId}")]
    public async Task<ActionResult<TagReviewerDto?>> UpdateTagReviewer(
        Guid containerReviewerId,
        [NotEmptyGuid] Guid tagReviewerId,
        [Required] UpdateTagReviewerDto updateTagReviewerDto)
    {
        var reviewerState = TagReviewerAdapter.MapTagReviewerStateDtoToModel(updateTagReviewerDto.State);

        var result = await _tagReviewerService.UpdateTagReviewer(tagReviewerId, Utils.GetAzureUniqueId(HttpContext.User), reviewerState);

        var displayName = await _userService.GetDisplayName(result.UserId);

        return result.ToDto(displayName);
    }
}
