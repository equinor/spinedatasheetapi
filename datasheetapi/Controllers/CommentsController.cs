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

    public CommentsController(ILoggerFactory loggerFactory, ICommentService commentService, IMapper mapper)
    {
        _logger = loggerFactory.CreateLogger<CommentsController>();
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpPut("{conversationId}/comments/{commentId}", Name = "UpdateComment")]
    public async Task<ActionResult<CommentDto>> UpdateComment([FromRoute] Guid conversationId, [FromRoute] Guid commentId, MessageDto newCommentDto)
    {
        if (commentId == Guid.Empty || newCommentDto == null)
        {
            return BadRequest();
        }
        var azureUniqueId = GetAzureUniqueId();
        try
        {
            var newComment = _mapper.Map<Message>(newCommentDto);
            newComment.Id = commentId;
            var comment = await _commentService.UpdateComment(azureUniqueId, newComment);
            return Ok(comment);
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

    // TODO; we need to verify reviewid?
    [HttpPost("{conversationId}/comments", Name = "AddComment")]
    public async Task<ActionResult<GetMessageDto>> AddComment([FromRoute] Guid reviewId, [FromRoute] Guid conversationId, MessageDto comment)
    {

        var message = _mapper.Map<Message>(comment);

        message.ConversationId = conversationId;
        message.UserId = GetAzureUniqueId();

        var response = await _commentService.AddComment(message);
        return _mapper.Map<GetMessageDto>(response);
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
            return _mapper.Map<GetMessageDto>(comment);
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
            var comment = await _commentService.GetComments(conversationId);
            if (comment == null)
            {
                return NotFound();
            }
            // TODO: fix the commenter name
            return _mapper.Map<List<GetMessageDto>>(comment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comment with id {id}", conversationId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    // TODO: Fix the body object 
    [HttpPost(Name = "CreateConversation")]
    public async Task<ActionResult<GetConversationDto>> CreateConversation([FromRoute] Guid reviewId, [FromBody] CreateCommentDto comment)
    {
        var azureUniqueId = GetAzureUniqueId();

        var conversation = _mapper.Map<CreateCommentDto, Conversation>(comment);
        conversation.TagDataReviewId = reviewId;

        //TODO: Move to common place
        Message message = _mapper.Map<Message>(comment);
        message.UserId = azureUniqueId;
        conversation.Messages = new List<Message> { message };

        var participant = _mapper.Map<Participant>(conversation);
        participant.UserId = azureUniqueId;

        conversation.Participants = new List<Participant> { participant };

        var savedConversation = await _commentService.CreateConversation(conversation);

        return _mapper.Map<Conversation, GetConversationDto>(savedConversation);
    }

    [HttpGet("{conversationId}", Name = "GetConversation")]
    public async Task<ActionResult<GetConversationDto>> GetConversation(Guid conversationId)
    {
        // TODO; do we need to avalid the reviewId also ?
        if (conversationId == Guid.Empty)
        {
            return BadRequest();
        }
        try
        {
            var comment = await _commentService.GetConversation(conversationId);
            if (comment == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<Conversation, GetConversationDto>(comment);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comment with id {id}", conversationId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(Name = "GetConversations")]
    public async Task<ActionResult<List<GetConversationDto>>> GetConversations(Guid reviewId)
    {
        try
        {
            var response = await _commentService.GetConversations(reviewId);
            return _mapper.Map<List<Conversation>, List<GetConversationDto>>(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all comments");
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
}
