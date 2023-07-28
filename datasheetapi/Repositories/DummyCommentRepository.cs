namespace datasheetapi.Repositories;

public class DummyCommentRepository : ICommentRepository
{
    private readonly List<Comment> _comments = new();
    private readonly ILogger<ContractService> _logger;

    public DummyCommentRepository(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ContractService>();
    }

    public async Task<Comment?> GetComment(Guid id)
    {
        return await Task.Run(() => _comments.Find(c => c.Id == id));
    }

    public async Task<List<Comment>> GetComments()
    {
        return await Task.Run(() => _comments);
    }

    public async Task<bool> DeleteComment(Comment comment)
    {
        return await Task.Run(() => _comments.Remove(comment));
    }

    public async Task<List<Comment>> GetCommentsForTagReview(Guid tagId)
    {
        return await Task.Run(() => _comments.Where(c => c.TagDataReviewId == tagId).ToList());
    }

    public async Task<List<Comment>> GetCommentsForRevisionContainerReview(Guid tagId)
    {
        return await Task.Run(() => _comments.Where(c => c.RevisionContainerReviewId == tagId).ToList());
    }

    public async Task<List<Comment>> GetCommentsForTagReviews(List<Guid?> tagIds)
    {
        return await Task.Run(() => _comments.Where(c => tagIds.Contains(c.TagDataReviewId)).ToList());
    }

    public async Task<List<Comment>> GetCommentsForRevisionContainerReviews(List<Guid?> tagIds)
    {
        return await Task.Run(() => _comments.Where(c => tagIds.Contains(c.RevisionContainerReviewId)).ToList());
    }


    public async Task<Comment> AddComment(Comment comment)
    {
        comment.Id = Guid.NewGuid();
        comment.CreatedDate = DateTime.UtcNow;
        comment.ModifiedDate = DateTime.UtcNow;
        _comments.Add(comment);
        return await Task.Run(() => _comments.Last());
    }
}
