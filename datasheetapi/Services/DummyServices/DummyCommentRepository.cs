namespace datasheetapi.Services;

public class DummyCommentRepository : ICommentRepository
{
    private List<Comment> _comments = new();
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

    public async Task<List<Comment>> GetCommentsForTag(Guid tagId)
    {
        return await Task.Run(() => _comments.Where(c => c.TagDataId == tagId).ToList());
    }

    public async Task<List<Comment>> GetCommentsForTags(List<Guid> tagIds)
    {
        return await Task.Run(() => _comments.Where(c => tagIds.Contains(c.TagDataId)).ToList());
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
