namespace datasheetapi.Services;

public interface ICommentRepository
{
    Task<Comment> AddComment(Comment comment);
    Task<Comment?> GetComment(Guid id);
    Task<List<Comment>> GetComments();
    Task<List<Comment>> GetCommentsForTag(Guid tagId);
    Task<List<Comment>> GetCommentsForTags(List<Guid> tagId);
}
