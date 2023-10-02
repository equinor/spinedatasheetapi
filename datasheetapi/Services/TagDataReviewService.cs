using datasheetapi.Exceptions;
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

    public async Task<TagDataReview> GetTagDataReview(Guid reviewId)
    {
        var review = await _reviewRepository.GetTagDataReview(reviewId);
        return review ?? throw new NotFoundException($"Invalid reviewId - {reviewId}.");
    }

    public async Task<bool> AnyTagDataReview(Guid reviewId)
    {
        var exists = await _reviewRepository.AnyTagDataReview(reviewId);
        return exists;
    }

    public async Task<List<TagDataReview>> GetTagDataReviews()
    {
        var reviews = await _reviewRepository.GetTagDataReviews();
        return reviews;
    }

    public async Task<List<TagDataReview>> GetTagDataReviewsForTag(string tagNo)
    {
        var reviews = await _reviewRepository.GetTagDataReviewsForTag(tagNo);
        return reviews;
    }

    public async Task<List<TagDataReview>> GetTagDataReviewsForTags(List<string> tagNos)
    {
        var reviews = await _reviewRepository.GetTagDataReviewsForTags(tagNos);
        return reviews;
    }

    public async Task<TagDataReview> CreateTagDataReview(TagDataReview review, Guid azureUniqueId)
    {
        if (string.IsNullOrEmpty(review.TagNo)) { throw new BadRequestException("TagNo is required"); }
        var _ = await _tagDataService.GetTagDataByTagNo(review.TagNo) ??
            throw new NotFoundException($"Invalid tag data id: {review.TagNo}");
        review.ApproverId = azureUniqueId;

        TagDataReview savedReview = await _reviewRepository.AddTagDataReview(review);
        return savedReview;
    }
}
