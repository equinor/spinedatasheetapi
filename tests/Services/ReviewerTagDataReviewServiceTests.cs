using datasheetapi.Exceptions;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Moq;

namespace tests.Services;
public class ReviewerTagDataReviewServiceTests
{
    private readonly Mock<ITagDataReviewService> _reviewService = new();
    private readonly Mock<IReviewerRepository> _reviewerTagDataReviewRepository = new();

    private readonly ReviewerService _reviewerTagDataReviewService;

    public ReviewerTagDataReviewServiceTests()
    {
        _reviewerTagDataReviewService = new ReviewerService(
            _reviewService.Object,
            _reviewerTagDataReviewRepository.Object);
    }

    [Fact]
    public async Task CreateReviewerTagDataReview_ThrowsIfReviewDoesNotExist()
    {
        var reviewerTagDataReview = new Reviewer { Status = 0, ReviewerId = Guid.NewGuid() };
        var reviewId = Guid.NewGuid();

        _reviewService.Setup(x => x.AnyTagDataReview(reviewId)).ThrowsAsync(new NotFoundException($"Invalid reviewId - {reviewId}."));

        await Assert.ThrowsAsync<NotFoundException>(() => _reviewerTagDataReviewService.CreateReviewerTagDataReviews(reviewId, reviewerTagDataReview));
    }

    [Fact]
    public async Task CreateReviewerTagDataReview_RunsOkayWithCorrectInput()
    {
        var reviewerTagDataReview = new Reviewer { Status = ReviewStatusEnum.Reviewed, ReviewerId = Guid.NewGuid() };
        var reviewId = Guid.NewGuid();

        _reviewService.Setup(x => x.AnyTagDataReview(reviewId)).ReturnsAsync(true);

        _reviewerTagDataReviewRepository.Setup(x => x.CreateReviewerTagDataReviews(reviewerTagDataReview)).ReturnsAsync(reviewerTagDataReview);

        var result = await _reviewerTagDataReviewService.CreateReviewerTagDataReviews(reviewId, reviewerTagDataReview);

        Assert.NotNull(result);
        Assert.Equal(reviewerTagDataReview.Status, result.Status);
        Assert.Equal(reviewerTagDataReview.TagDataReviewId, reviewId);
        _reviewService.Verify(x => x.AnyTagDataReview(reviewId), Times.Once);
        _reviewerTagDataReviewRepository.Verify(x => x.CreateReviewerTagDataReviews(reviewerTagDataReview), Times.Once);
    }
}
