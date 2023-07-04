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
    private readonly CommentService _commentService;
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(ILoggerFactory loggerFactory, CommentService commentService)
    {
        _logger = loggerFactory.CreateLogger<CommentsController>();
        _commentService = commentService;
    }

    [HttpGet("{id}", Name = "GetComment")]
    public async Task<ActionResult<Comment>> GetComment([FromQuery] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest();
        }
        
        try
        {
            var comment = await _commentService.GetComment(id);
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
    public async Task<ActionResult<List<Comment>>> GetComments()
    {
        try
        {
            return await _commentService.GetComments();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all comments");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("tag/{id}", Name = "GetCommentsForTag")]
    public async Task<ActionResult<List<Comment>>> GetCommentsForTag(Guid id)
    {
        try
        {
            return await _commentService.GetCommentsForTag(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comments for tag with id {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(Name = "CreateComment")]
    public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment comment)
    {
        var httpContext = HttpContext;
        var user = httpContext.User;
        var fusionIdentity = user.Identities.FirstOrDefault(i => i is Fusion.Integration.Authentication.FusionIdentity) as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ?? throw new Exception("Could not get Azure Unique Id");

        if (comment == null) { return BadRequest("Comment cannot be null"); }
        if (comment.Text == null) { return BadRequest("Comment text cannot be null"); }

        try
        {
            return await _commentService.CreateComment(comment, azureUniqueId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating comment", comment);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
