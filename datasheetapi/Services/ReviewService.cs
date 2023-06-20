namespace datasheetapi.Services;

public class ReviewService
{
    private readonly ILogger<ReviewService> _logger;
    private readonly ITagDataService _tagDataService;

    private readonly DummyReviewRepository _reviewRepository;

    public ReviewService(ILoggerFactory loggerFactory, DummyReviewRepository reviewRepository,
        ITagDataService tagDataService)
    {
        _reviewRepository = reviewRepository;
        _tagDataService = tagDataService;
        _logger = loggerFactory.CreateLogger<ReviewService>();
    }

    public async Task<Review?> GetReview(Guid id)
    {
        var review = await _reviewRepository.GetReview(id);
        return review;
    }

    public async Task<List<Review>> GetReviews()
    {
        var reviews = await _reviewRepository.GetReviews();
        return reviews;
    }

    public async Task<List<Review>> GetReviewsForProject(Guid projectId)
    {
        return await Task.Run(() => new List<Review>());
    }

    public async Task<List<Review>> GetReviewsForTag(Guid tagId)
    {
        var comments = await _reviewRepository.GetReviewsForTag(tagId);
        return comments;
    }

    public async Task<List<Review>> GetReviewsForTags(List<Guid> tagIds)
    {
        var comments = await _reviewRepository.GetReviewsForTags(tagIds);
        return comments;
    }

    public async Task<Review> CreateReview(Review review, Guid azureUniqueId)
    {
        _ = await _tagDataService.GetTagDataById(review.TagId) ?? throw new Exception("Invalid tag");
        review.UserId = azureUniqueId;

        Review? savedReview = await _reviewRepository.AddComment(review) ?? throw new Exception("Invalid comment");
        return savedReview;
    }
}
