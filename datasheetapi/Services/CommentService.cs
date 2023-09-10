using AutoMapper;

using datasheetapi.Adapters;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class CommentService : ICommentService
{
    private readonly ILogger<ContractService> _logger;
    private readonly ITagDataService _tagDataService;
    private readonly ITagDataReviewService _tagDataReviewService;
    private readonly IRevisionContainerReviewService _revisionContainerReviewService;
    private readonly ICommentRepository _commentRepository;
    private readonly IAzureUserCacheService _azureUserCacheService;
    private readonly IFusionService _fusionService;

    public CommentService(ILoggerFactory loggerFactory,
        ITagDataService tagDataService,
        ICommentRepository commentRepository,
        IAzureUserCacheService azureUserCacheService,
        IFusionService fusionService,
        ITagDataReviewService tagDataReviewService,
        IRevisionContainerReviewService revisionContainerReviewService)
    {
        _logger = loggerFactory.CreateLogger<ContractService>();
        _tagDataService = tagDataService;
        _commentRepository = commentRepository;
        _azureUserCacheService = azureUserCacheService;
        _fusionService = fusionService;
        _tagDataReviewService = tagDataReviewService;
        _revisionContainerReviewService = revisionContainerReviewService;
    }

    public async Task<Message?> GetComment(Guid id)
    {
        var comment = await _commentRepository.GetComment(id);
        return comment;
    }

    public async Task<Conversation?> GetConversation(Guid conversationId)
    {
        var comment = await _commentRepository.GetConversation(conversationId);
        return comment;
    }

    public async Task<List<Message>?> GetComments(Guid converstionId)
    {
        var comments = await _commentRepository.GetComments(converstionId);
        return comments;
    }

    public async Task<List<Conversation>> GetConversations(Guid reviewId)
    {
        var comments = await _commentRepository.GetConversations(reviewId);
        return comments;
    }

    public async Task DeleteComment(Guid id, Guid azureUniqueId)
    {
        if (azureUniqueId == Guid.Empty) { throw new Exception("Invalid azure unique id"); }
        var existingComment = await _commentRepository.GetComment(id) ?? throw new Exception("Invalid comment id");
        if (existingComment.UserId != azureUniqueId) { throw new Exception("User not author of this comment"); }
        if (existingComment.SoftDeleted) { throw new Exception("Cannot update deleted comment"); }

        existingComment.SoftDeleted = true;
        await _commentRepository.UpdateComment(existingComment);
    }

    public async Task<Message> UpdateComment(Guid commentId, Message updatedComment)
    {
        var existingComment = await _commentRepository.GetComment(commentId)
                ?? throw new Exception($"Comment with id {commentId} not found");

        if (existingComment.UserId != updatedComment.UserId) { 
            throw new Exception("User not author of this comment"); }

        existingComment.Text = updatedComment.Text;
        existingComment.IsEdited = true;
        return await _commentRepository.UpdateComment(existingComment);
    }

    private static bool ValidateProperty<T>(string propertyName)
    where T : class, new()
    {
        var obj = new T();
        var propertyInfo = obj.GetType().GetProperty(propertyName);

        return propertyInfo != null;
    }

    public async Task<Message> AddComment(Guid conversationId, Message message)
    {
        var conversation = await _commentRepository.GetConversation(conversationId) 
                ?? throw new Exception("Invalid conversation");
        
        //Check with conversationId whether works or not
        message.SetConversation(conversation);

        return await _commentRepository.AddComment(message);
    }

    public async Task<Conversation> CreateConversation(Conversation conversation)
    {
        var tagDataReview = await _tagDataReviewService.GetTagDataReview((Guid)conversation.TagDataReviewId)
        ?? throw new Exception("Invalid tag data review");

        conversation.SetTagDataReview(tagDataReview);

        return await _commentRepository.CreateConversation(conversation);
    }

    public async Task<string> GetUserName(Guid userId)
    {
        var azureUser = await _azureUserCacheService.GetAzureUserAsync(userId);
        if (azureUser == null)
        {
            var user = await _fusionService.ResolveUserFromPersonId(userId);
            if (user != null)
            {
                azureUser = new AzureUser { AzureUniqueId = userId, Name = user?.Name };
                _azureUserCacheService.AddAzureUser(azureUser);
            }
        }
        if (azureUser != null)
        {
            return azureUser.Name ?? "Unknown user";
        }
        else
        {
            throw new Exception("Unable to find the username for the userId: " + userId);
        }
    }

    public async Task<Dictionary<Guid, string>> GetUserIdUserName(List<Guid> userIds)
    {
        var userIdUserNameMap = new Dictionary<Guid, string>();
        foreach (Guid userId in  userIds) 
        {
            var userName = await GetUserName(userId);
            userIdUserNameMap.TryAdd(userId, userName);
        }
        return userIdUserNameMap;
    }
}
