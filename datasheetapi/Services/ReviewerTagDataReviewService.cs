using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ReviewerTagDataReviewService
{
    private readonly ITagDataReviewService _reviewService;

    private readonly ReviewerTagDataReviewRepository _reviewerTagDataReviewRepository;

    public ReviewerTagDataReviewService(
        ITagDataReviewService reviewService,
        ReviewerTagDataReviewRepository reviewerTagDataReviewRepository)
    {
        _reviewService = reviewService;
        _reviewerTagDataReviewRepository = reviewerTagDataReviewRepository;
    }

    public async Task<ReviewerTagDataReview> CreateReviewerTagDataReview(Guid reviewId, ReviewerTagDataReview review)
    {
        if (!await _reviewService.AnyTagDataReview(reviewId))
        {

            throw new NotFoundException($"Invalid reviewId - {reviewId}.");
        }

        review.TagDataReviewId = reviewId;

        var result = await _reviewerTagDataReviewRepository.CreateReviewerTagDataReview(review);

        return result;
    }
}
