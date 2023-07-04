namespace datasheetapi.Services
{
    public interface ICommentService
    {
        Task<Comment?> GetComment(Guid id);
        Task<List<Comment>> GetComments();
        Task<List<Comment>> GetCommentsForTagReview(Guid tagId);
        Task<List<Comment>> GetCommentsForTagReviews(List<Guid?> tagIds);
        Task<Comment> CreateTagDataReviewComment(Comment comment, Guid azureUniqueId);
        Task<Comment> CreateRevisionContainerReviewComment(Comment comment, Guid azureUniqueId);
    }
}