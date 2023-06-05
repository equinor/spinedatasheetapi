namespace datasheetapi.Services;

public class CommentService
{
    private readonly ILogger<ContractService> _logger;
    private readonly IDatasheetService _datasheetService;
    private readonly ICommentRepository _commentRepository;

    public CommentService(ILoggerFactory loggerFactory, IDatasheetService datasheetService, ICommentRepository commentRepository)
    {
        _logger = loggerFactory.CreateLogger<ContractService>();
        _datasheetService = datasheetService;
        _commentRepository = commentRepository;
    }

    public async Task<Comment?> GetComment(Guid id)
    {
        return await _commentRepository.GetComment(id);
    }

    public async Task<List<Comment>> GetComments()
    {
        return await _commentRepository.GetComments();
    }

    public async Task<List<Comment>> GetCommentsForTag(Guid tagId)
    {
        return await _commentRepository.GetCommentsForTag(tagId);
    }

    public async Task<List<Comment>> GetCommentsForTags(List<Guid> tagIds)
    {
        return await _commentRepository.GetCommentsForTags(tagIds);
    }

    public async Task<Comment> CreateComment(Comment comment, Guid azureUniqueId)
    {
        var tagData = await _datasheetService.GetDatasheetById(comment.TagDataId) ?? throw new Exception("Invalid tag");
        Comment? savedComment = null;
        comment.UserId = azureUniqueId;

        if (comment.CommentLevel == CommentLevel.Tag)
        {
            savedComment = await _commentRepository.AddComment(comment);
        }
        else if (comment.CommentLevel == CommentLevel.PurchaserRequirement && comment.Property != null)
        {
            if (ValidateProperty<PurchaserRequirement>(comment.Property))
            {
                savedComment = await _commentRepository.AddComment(comment);
            }
        }
        else if (comment.CommentLevel == CommentLevel.SupplierOfferedValue && comment.Property != null)
        {
            if (ValidateProperty<SupplierOfferedProduct>(comment.Property))
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

    private static bool ValidateProperty<T>(string propertyName)
    where T : class, new()
    {
        var obj = new T();
        var propertyInfo = obj.GetType().GetProperty(propertyName);

        return propertyInfo != null;
    }
}
