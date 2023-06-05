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

    public CommentsController(CommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("{id}", Name = "GetComment")]
    public async Task<ActionResult<Comment?>> GetComment([FromQuery] Guid id)
    {
        return await _commentService.GetComment(id);
    }

    [HttpGet(Name = "GetComments")]
    public async Task<ActionResult<List<Comment>>> GetComments()
    {
        return await _commentService.GetComments();
    }

    [HttpGet("tag/{id}", Name = "GetCommentsForTag")]
    public async Task<ActionResult<List<Comment>>> GetCommentsForTag(Guid id)
    {
        return await _commentService.GetCommentsForTag(id);
    }

    [HttpPost(Name = "CreateComment")]
    public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment comment)
    {
        var httpContext = HttpContext;
        var user = httpContext.User;
        var fusionIdentity = user.Identities.FirstOrDefault(i => i is Fusion.Integration.Authentication.FusionIdentity) as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ?? throw new Exception("Could not get Azure Unique Id");
        return await _commentService.CreateComment(comment, azureUniqueId);
    }
}
