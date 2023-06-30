namespace datasheetapi.Services;

public class TagDataReviewService
{
    private readonly ILogger<TagDataReviewService> _logger;
    private readonly ITagDataService _tagDataService;

    private readonly ITagDataReviewRepository _reviewRepository;

    public TagDataReviewService(ILoggerFactory loggerFactory, ITagDataReviewRepository reviewRepository,
        ITagDataService tagDataService)
    {
        _reviewRepository = reviewRepository;
        _tagDataService = tagDataService;
        _logger = loggerFactory.CreateLogger<TagDataReviewService>();
    }

    public async Task<TagDataReview?> GetTagDataReview(Guid id)
    {
        var review = await _reviewRepository.GetTagDataReview(id);
        return review;
    }

    public async Task<List<TagDataReview>> GetTagDataReviews()
    { 
        var reviews = await _reviewRepository.GetTagDataReviews();
        return reviews;
    }

    public async Task<List<TagDataReview>> GetTagDataReviewsForProject(Guid projectId)
    {
        return await Task.Run(() => new List<TagDataReview>());
    }

    public async Task<List<TagDataReview>> GetReviewsForTag(Guid tagId)
    {
        var comments = await _reviewRepository.GetTagDataReviewsForTag(tagId);
        return comments;
    }

    public async Task<List<TagDataReview>> GetTagDataReviewsForTags(List<Guid> tagIds)
    {
        var comments = await _reviewRepository.GetTagDataReviewsForTags(tagIds);
        return comments;
    }

    public async Task<TagDataReview> CreateTagDataReview(TagDataReview review, Guid azureUniqueId)
    {
        var tagData = await _tagDataService.GetTagDataById(review.TagId) ?? throw new Exception("Invalid tag");
        review.ApproverId = azureUniqueId;

        TagDataReview? savedReview = await _reviewRepository.AddTagDataReview(review) ?? throw new Exception("Invalid comment");
        tagData.Review = savedReview;
        return savedReview;
    }
}
