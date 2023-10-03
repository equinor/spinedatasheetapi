using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ReviewerTagDataReviewService
{
    private readonly ILogger<ReviewerTagDataReviewService> _logger;
    private readonly ITagDataService _tagDataService;
    private readonly ITagDataReviewService _reviewService;

    private readonly ITagDataReviewRepository _reviewRepository;

    private readonly ReviewerTagDataReviewRepository _reviewerTagDataReviewRepository;

    public ReviewerTagDataReviewService(ILoggerFactory loggerFactory, ITagDataReviewRepository reviewRepository,
        ITagDataService tagDataService, ITagDataReviewService reviewService, ReviewerTagDataReviewRepository reviewerTagDataReviewRepository)
    {
        _reviewRepository = reviewRepository;
        _tagDataService = tagDataService;
        _logger = loggerFactory.CreateLogger<ReviewerTagDataReviewService>();
        _reviewService = reviewService;
        _reviewerTagDataReviewRepository = reviewerTagDataReviewRepository;
    }

    public async Task<ReviewerTagDataReview> CreateReviewerTagDataReview(Guid reviewId, ReviewerTagDataReview review)
    {
        if (!await _reviewService.AnyTagDataReview(reviewId)) { throw new NotFoundException($"Invalid reviewId - {reviewId}."); }

        review.TagDataReviewId = reviewId;

        var result = await _reviewerTagDataReviewRepository.CreateReviewerTagDataReview(review);

        return result;
    }
}
