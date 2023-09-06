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

    public async Task<Comment?> GetComment(Guid id)
    {
        var comment = await _commentRepository.GetComment(id);
        if (comment == null) { return null; }
        await AddUserNameToComment(comment);
        return comment;
    }

    public async Task<CommentDto?> GetCommentDto(Guid id)
    {
        var comment = await GetComment(id);
        return comment?.ToDtoOrNull();
    }

    public async Task<List<Comment>> GetComments()
    {
        var comments = await _commentRepository.GetComments();
        await AddUserNameToComments(comments);
        return comments;
    }


    public async Task<List<CommentDto>> GetCommentDtos()
    {
        var comments = await GetComments();
        return comments.ToDto();
    }

    public async Task<List<Comment>> GetCommentsForTagReview(Guid tagId)
    {
        var comments = await _commentRepository.GetCommentsForTagReview(tagId);
        await AddUserNameToComments(comments);
        return comments;
    }

    public async Task<List<CommentDto>> GetCommentDtosForTagReview(Guid tagId)
    {
        var comments = await GetCommentsForTagReview(tagId);
        return comments.ToDto();
    }

    public async Task<List<Comment>> GetCommentsForTagReviews(List<Guid?> tagIds)
    {
        var comments = await _commentRepository.GetCommentsForTagReviews(tagIds);
        await AddUserNameToComments(comments);
        return comments;
    }

    private async Task AddUserNameToComments(List<Comment> comments)
    {
        foreach (var comment in comments)
        {
            await AddUserNameToComment(comment);
        }
    }

    private async Task AddUserNameToComment(Comment comment)
    {
        var azureUser = await _azureUserCacheService.GetAzureUserAsync(comment.UserId);
        if (azureUser == null)
        {
            var user = await _fusionService.ResolveUserFromPersonId(comment.UserId);
            if (user != null)
            {
                azureUser = new AzureUser { AzureUniqueId = comment.UserId, Name = user?.Name };
                _azureUserCacheService.AddAzureUser(azureUser);
            }
        }
        if (azureUser != null)
        {
            comment.CommenterName = azureUser.Name ?? "Unknown user";
        }
    }

    public async Task<Comment> CreateTagDataReviewComment(Comment comment, Guid azureUniqueId)
    {
        if (comment.TagDataReviewId == null || comment.TagDataReviewId == Guid.Empty) { throw new Exception("Invalid tag data review id"); }
        var tagDataReview = await _tagDataReviewService.GetTagDataReview((Guid)comment.TagDataReviewId) ?? throw new Exception("Invalid tag data review");

        comment.SetTagDataReview(tagDataReview);

        return await CreateComment(comment, azureUniqueId);
    }

    public async Task<Comment> CreateRevisionContainerReviewComment(Comment comment, Guid azureUniqueId)
    {
        if (comment.RevisionContainerReviewId == null || comment.RevisionContainerReviewId == Guid.Empty) { throw new Exception("Invalid revision container review id"); }
        var revisionContainerReview = await _revisionContainerReviewService.GetRevisionContainerReview((Guid)comment.RevisionContainerReviewId) ?? throw new Exception("Invalid revision container review");

        comment.SetRevisionContainerReview(revisionContainerReview);

        return await CreateComment(comment, azureUniqueId);
    }

    private async Task<Comment> CreateComment(Comment comment, Guid azureUniqueId)
    {
        Comment? savedComment = null;
        comment.UserId = azureUniqueId;

        if (comment.CommentLevel == CommentLevel.Tag)
        {
            savedComment = await _commentRepository.AddComment(comment);
        }
        else if (comment.CommentLevel == CommentLevel.PurchaserRequirement && comment.Property != null)
        {
            if (ValidateProperty<InstrumentPurchaserRequirement>(comment.Property))
            {
                savedComment = await _commentRepository.AddComment(comment);
            }
        }
        else if (comment.CommentLevel == CommentLevel.SupplierOfferedValue && comment.Property != null)
        {
            if (ValidateProperty<InstrumentSupplierOfferedProduct>(comment.Property))
            {
                savedComment = await _commentRepository.AddComment(comment);
            }
        }

        if (savedComment == null)
        {
            throw new Exception("Invalid comment");
        }

        return savedComment;
    }

    public async Task DeleteComment(Guid id, Guid azureUniqueId)
    {
        if (azureUniqueId == Guid.Empty) { throw new Exception("Invalid azure unique id"); }
        var existingComment = await GetComment(id) ?? throw new Exception("Invalid comment id");
        if (existingComment.UserId != azureUniqueId) { throw new Exception("User not author of this comment"); }
        if (existingComment.SoftDeleted) { throw new Exception("Cannot update deleted comment"); }

        existingComment.SoftDeleted = true;
        await _commentRepository.UpdateComment(existingComment);
    }

    public async Task<CommentDto?> UpdateComment(Guid azureUniqueId, Comment updatedComment)
    {
        var existingComment = await GetComment(updatedComment.Id) ?? throw new Exception($"Comment with id {updatedComment.Id} not found");
        if (existingComment.UserId != azureUniqueId) { throw new Exception("User not author of this comment"); }

        existingComment.Text = updatedComment.Text;
        existingComment.IsEdited = true;
        var comment = await _commentRepository.UpdateComment(existingComment);
        return comment.ToDtoOrNull();
    }

    private static bool ValidateProperty<T>(string propertyName)
    where T : class, new()
    {
        var obj = new T();
        var propertyInfo = obj.GetType().GetProperty(propertyName);

        return propertyInfo != null;
    }

    public async Task<CommentDto> CreateTagDataReviewComment(CommentDto comment, Guid azureUniqueId)
    {
        var commentModel = comment.ToModelOrNull() ?? throw new Exception("Invalid comment");
        var savedComment = await CreateTagDataReviewComment(commentModel, azureUniqueId);
        return savedComment.ToDtoOrNull() ?? throw new Exception("Invalid comment");
    }

    public async Task<CommentDto> CreateRevisionContainerReviewComment(CommentDto comment, Guid azureUniqueId)
    {
        var commentModel = comment.ToModelOrNull() ?? throw new Exception("Invalid comment");
        var savedComment = await CreateRevisionContainerReviewComment(commentModel, azureUniqueId);
        return savedComment.ToDtoOrNull() ?? throw new Exception("Invalid comment");
    }
}
