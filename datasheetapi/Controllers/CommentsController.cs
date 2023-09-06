using AutoMapper;
using datasheetapi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace datasheetapi.Controllers;

[ApiController]
[Route("/tag/reviews/{reviewId}/conversations")]
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
    private readonly IMapper _mapper;

    public CommentsController(ILoggerFactory loggerFactory,
                            ICommentService commentService)
    {
        _logger = loggerFactory.CreateLogger<CommentsController>();
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpPost(Name = "CreateConversation")]
    public async Task<ActionResult<ConversationDto>> CreateConversation(
        [FromRoute] Guid reviewId, [FromBody] CreateCommentDto comment)
    {
        if (comment.Property != null)
        {
            if (!ValidateProperty<InstrumentPurchaserRequirement>(comment.Property) &&
               !ValidateProperty<InstrumentSupplierOfferedProduct>(comment.Property))
            {
                return BadRequest($"Not supported property: {comment.Property}");
            }
        }

        var azureUniqueId = GetAzureUniqueId();
        try
        {
            var savedConversation = await _commentService.CreateConversation(
                comment.ToModel(reviewId, azureUniqueId));

            var userIdNameMap = await _commentService.GetUserIdUserName(
                savedConversation.Participants.Select(p => p.UserId).ToList());

            return savedConversation.ToDto(userIdNameMap);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating the conversation");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{conversationId}", Name = "GetConversation")]
    public async Task<ActionResult<ConversationDto>> GetConversation(Guid conversationId)
    {
        // TODO; do we need to avalid the reviewId also ?
        if (conversationId == Guid.Empty)
        {
            return BadRequest();
        }
        try
        {
            var conversation = await _commentService.GetConversation(conversationId);
            if (conversation == null)
            {
                return NotFound();
            }
            var userIdNameMap = await _commentService.GetUserIdUserName(
                conversation.Participants.Select(p => p.UserId).ToList());

            return conversation.ToDto(userIdNameMap);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comment with id {id}", conversationId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(Name = "GetConversations")]
    public async Task<ActionResult<List<ConversationDto>>> GetConversations(Guid reviewId)
    {
        try
        {
            var conversations = await _commentService.GetConversations(reviewId);

            var userIds = conversations.SelectMany(conversation => 
                            conversation.Participants.Select(p => p.UserId)).ToList();
            var userIdNameMap = await _commentService.GetUserIdUserName(userIds);
            
            return conversations.Select(conversation => conversation.ToDto(userIdNameMap)).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all comments");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    // TODO; we need to verify reviewid?
    [HttpPost("{conversationId}/comments", Name = "AddComment")]
    public async Task<ActionResult<GetMessageDto>> AddComment([FromRoute] Guid reviewId,
         [FromRoute] Guid conversationId, MessageDto comment)
    {

        var message = comment.ToMessageModel(GetAzureUniqueId());

        var savedComment = await _commentService.AddComment(conversationId, message);

        return savedComment.ToMessageDto(await _commentService.GetUserName(savedComment.UserId));
    }

    [HttpGet("{conversationId}/comments/{commentId}", Name = "GetComment")]
    public async Task<ActionResult<GetMessageDto>> GetComment(Guid commentId)
    {
        if (commentId == Guid.Empty)
        {
            return BadRequest();
        }
        try
        {
            var comment = await _commentService.GetComment(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            var username = await _commentService.GetUserName(comment.UserId);
            return comment.ToMessageDto(username);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comment with id {id}", commentId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{conversationId}/comments", Name = "GetComments")]
    public async Task<ActionResult<List<GetMessageDto>>> GetComments(Guid conversationId)
    {
        if (conversationId == Guid.Empty)
        {
            return BadRequest();
        }
        try
        {
            var comments = await _commentService.GetComments(conversationId);
            if (comments == null)
            {
                return NotFound();
            }

            var userIdNameMap = await _commentService.GetUserIdUserName(
                    comments.Select(c => c.UserId).ToList());
            
            return comments.ToMessageDtos(userIdNameMap);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comment with id {id}", conversationId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{conversationId}/comments/{commentId}", Name = "UpdateComment")]
    public async Task<ActionResult<GetMessageDto>> UpdateComment([FromRoute] Guid conversationId,
                                                            [FromRoute] Guid commentId,
                                                            MessageDto newCommentDto)
    {
        if (commentId == Guid.Empty || newCommentDto == null)
        {
            return BadRequest();
        }
        try
        {
            var newComment = newCommentDto.ToMessageModel(GetAzureUniqueId());

            var comment = await _commentService.UpdateComment(commentId, newComment);

            var userName = await _commentService.GetUserName(comment.UserId);
            return comment.ToMessageDto(userName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error editing comment", commentId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{conversationId}/comments/{commentId}", Name = "DeleteComment")]
    public async Task<ActionResult> DeleteComment([FromRoute] Guid conversationId, [FromRoute] Guid commentId)
    {
        var azureUniqueId = GetAzureUniqueId();

        if (commentId == Guid.Empty)
        {
            return BadRequest();
        }

        try
        {
            await _commentService.DeleteComment(commentId, azureUniqueId);
            return NoContent();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting comment", commentId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private Guid GetAzureUniqueId()
    {
        var httpContext = HttpContext;
        var user = httpContext.User;
        var fusionIdentity = user.Identities.FirstOrDefault(i => i is Fusion.Integration.Authentication.FusionIdentity) as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ?? throw new Exception("Could not get Azure Unique Id");
        return azureUniqueId;
    }

    private static bool ValidateProperty<T>(string propertyName)
    where T : class, new()
    {
        var obj = new T();
        var propertyInfo = obj.GetType().GetProperty(propertyName);

        return propertyInfo != null;
    }
}
