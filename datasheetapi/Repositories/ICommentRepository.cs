namespace datasheetapi.Repositories;

public interface ICommentRepository
{
    Task<Comment> AddComment(Comment comment);
    Task<Comment?> GetComment(Guid id);
    Task<List<Comment>> GetComments();
    Task DeleteComment(Comment comment);
    Task<Comment> UpdateComment(Comment oldComment);
    Task<List<Comment>> GetCommentsForTagReview(Guid tagId);
    Task<List<Comment>> GetCommentsForRevisionContainerReview(Guid tagId);
    Task<List<Comment>> GetCommentsForTagReviews(List<Guid?> tagIds);
    Task<List<Comment>> GetCommentsForRevisionContainerReviews(List<Guid?> tagIds);
}
