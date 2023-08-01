namespace datasheetapi.Services
{
    public interface ICommentService
    {
        Task<Comment?> GetComment(Guid id);
        Task<CommentDto?> GetCommentDto(Guid id);
        Task<List<Comment>> GetComments();
        Task<List<CommentDto>> GetCommentDtos();
        Task DeleteComment(Guid id, Guid azureUniqueId);
        Task<List<Comment>> GetCommentsForTagReview(Guid tagId);
        Task<List<CommentDto>> GetCommentDtosForTagReview(Guid tagId);
        Task<List<Comment>> GetCommentsForTagReviews(List<Guid?> tagIds);
        Task<CommentDto> CreateTagDataReviewComment(CommentDto comment, Guid azureUniqueId);
        Task<CommentDto> CreateRevisionContainerReviewComment(CommentDto comment, Guid azureUniqueId);
    }
}
