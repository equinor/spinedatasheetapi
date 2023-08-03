using datasheetapi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("comments")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
    ApplicationRole.Admin,
    ApplicationRole.ReadOnlyUser,
    ApplicationRole.User
)]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(ILoggerFactory loggerFactory, ICommentService commentService)
    {
        _logger = loggerFactory.CreateLogger<CommentsController>();
        _commentService = commentService;
    }

    public Guid GetAzureUniqueId()
    {
        var httpContext = HttpContext;
        var user = httpContext.User;
        var fusionIdentity = user.Identities.FirstOrDefault(i => i is Fusion.Integration.Authentication.FusionIdentity) as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ?? throw new Exception("Could not get Azure Unique Id");
        return azureUniqueId;
    }

    [HttpPut("{id}", Name = "UpdateComment")]
    public async Task<ActionResult<CommentDto>> UpdateComment(Guid id, CommentDto newComment)
    {
        var azureUniqueId = GetAzureUniqueId();

        if (id == Guid.Empty)
        {
            return BadRequest();
        }

        try
        {
            var comment = await _commentService.UpdateComment(id, azureUniqueId, newComment.Text);
            return Ok(comment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error editing comment", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id}", Name = "DeleteComment")]
    public async Task<ActionResult> DeleteComment(Guid id)
    {
        var azureUniqueId = GetAzureUniqueId();

        if (id == Guid.Empty)
        {
            return BadRequest();
        }

        try
        {
            await _commentService.DeleteComment(id, azureUniqueId);
            return Ok();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting comment", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}", Name = "GetComment")]
    public async Task<ActionResult<CommentDto>> GetComment([FromQuery] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest();
        }

        try
        {
            var comment = await _commentService.GetCommentDto(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comment with id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(Name = "GetComments")]
    public async Task<ActionResult<List<CommentDto>>> GetComments()
    {
        try
        {
            return await _commentService.GetCommentDtos();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all comments");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("tagreview/{id}", Name = "GetCommentsForTagReview")]
    public async Task<ActionResult<List<CommentDto>>> GetCommentsForTagReview(Guid id)
    {
        try
        {
            return await _commentService.GetCommentDtosForTagReview(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comments for tag review with id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(Name = "CreateComment")]
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CommentDto comment)
    {
        var azureUniqueId = GetAzureUniqueId();

        if (comment == null) { return BadRequest("Comment cannot be null"); }
        if (comment.Text == null) { return BadRequest("Comment text cannot be null"); }

        var commentType = IsTagReviewComment(comment);
        if (commentType == CommentType.Invalid) { return BadRequest("Comment needs to be either for tag data review or revision container review"); }

        try
        {
            if (commentType == CommentType.TagDataReview)
            {
                return await _commentService.CreateTagDataReviewComment(comment, azureUniqueId);
            }
            return await _commentService.CreateRevisionContainerReviewComment(comment, azureUniqueId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating comment", comment);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static CommentType IsTagReviewComment(CommentDto comment)
    {
        if (comment.TagDataReviewId != null && comment.TagDataReviewId != Guid.Empty
        && (comment.RevisionContainerReviewId == null || comment.RevisionContainerReviewId == Guid.Empty))
        {
            return CommentType.TagDataReview;
        }
        else if (comment.RevisionContainerReviewId != null && comment.RevisionContainerReviewId != Guid.Empty
        && (comment.TagDataReviewId == null || comment.TagDataReviewId == Guid.Empty))
        {
            return CommentType.RevisionContainerReview;
        }
        return CommentType.Invalid;
    }

    private enum CommentType
    {
        TagDataReview,
        RevisionContainerReview,
        Invalid
    }
}
