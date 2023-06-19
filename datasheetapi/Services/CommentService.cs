namespace datasheetapi.Services;

public class CommentService
{
    private readonly ILogger<ContractService> _logger;
    private readonly ITagDataService _datasheetService;
    private readonly ICommentRepository _commentRepository;
    private readonly IAzureUserCacheService _azureUserCacheService;
    private readonly IFusionService _fusionService;

    public CommentService(ILoggerFactory loggerFactory, ITagDataService datasheetService, ICommentRepository commentRepository,
        IAzureUserCacheService azureUserCacheService, IFusionService fusionService)
    {
        _logger = loggerFactory.CreateLogger<ContractService>();
        _datasheetService = datasheetService;
        _commentRepository = commentRepository;
        _azureUserCacheService = azureUserCacheService;
        _fusionService = fusionService;
    }

    public async Task<Comment?> GetComment(Guid id)
    {
        var comment = await _commentRepository.GetComment(id);
        await AddUserNameToCommentAsync(comment);
        return comment;
    }

    public async Task<List<Comment>> GetComments()
    {
        var comments = await _commentRepository.GetComments();
        await AddUserNameToCommentsAsync(comments);
        return comments;
    }

    public async Task<List<Comment>> GetCommentsForTag(Guid tagId)
    {
        var comments = await _commentRepository.GetCommentsForTag(tagId);
        await AddUserNameToCommentsAsync(comments);
        return comments;
    }

    public async Task<List<Comment>> GetCommentsForTags(List<Guid> tagIds)
    {
        var comments = await _commentRepository.GetCommentsForTags(tagIds);
        await AddUserNameToCommentsAsync(comments);
        return comments;
    }

    private async Task AddUserNameToCommentsAsync(List<Comment> comments)
    {
        foreach (var comment in comments)
        {
            await AddUserNameToCommentAsync(comment);
        }
    }

    private async Task AddUserNameToCommentAsync(Comment? comment)
    {
        if (comment == null) { return; }
        var azureUser = _azureUserCacheService.GetAzureUserAsync(comment.UserId);
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

    public async Task<Comment> CreateComment(Comment comment, Guid azureUniqueId)
    {
        var tagData = await _datasheetService.GetTagDataById(comment.TagDataId) ?? throw new Exception("Invalid tag");
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

    private static bool ValidateProperty<T>(string propertyName)
    where T : class, new()
    {
        var obj = new T();
        var propertyInfo = obj.GetType().GetProperty(propertyName);

        return propertyInfo != null;
    }
}
