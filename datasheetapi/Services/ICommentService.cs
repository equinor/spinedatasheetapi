public interface ICommentService
{
    Task<Comment?> GetComment(Guid id);
    Task<List<Comment>> GetComments();
    Task<List<Comment>> GetCommentsForTag(Guid tagId);
    Task<List<Comment>> GetCommentsForTags(List<Guid> tagIds);
}