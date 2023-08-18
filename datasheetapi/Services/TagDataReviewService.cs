using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class TagDataReviewService : ITagDataReviewService
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

    public async Task<List<TagDataReview>> GetTagDataReviewsForTag(string tagNo)
    {
        var comments = await _reviewRepository.GetTagDataReviewsForTag(tagNo);
        return comments;
    }

    public async Task<List<TagDataReview>> GetTagDataReviewsForTags(List<string> tagNos)
    {
        var comments = await _reviewRepository.GetTagDataReviewsForTags(tagNos);
        return comments;
    }

    public async Task<TagDataReview> CreateTagDataReview(TagDataReview review, Guid azureUniqueId)
    {
        if (string.IsNullOrEmpty(review.TagNo)) { throw new Exception("TagNo is required"); }
        var _ = await _tagDataService.GetTagDataByTagNo(review.TagNo) ?? throw new Exception($"Invalid tag data id: {review.TagNo}");
        review.ApproverId = azureUniqueId;

        TagDataReview savedReview = await _reviewRepository.AddTagDataReview(review);
        return savedReview;
    }
}
