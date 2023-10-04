using datasheetapi.Exceptions;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Moq;

namespace tests.Services;
public class ReviewerTagDataReviewServiceTests
{
    private readonly Mock<ITagDataReviewService> _reviewService = new();
    private readonly Mock<IReviewerTagDataReviewRepository> _reviewerTagDataReviewRepository = new();

    private readonly ReviewerTagDataReviewService _reviewerTagDataReviewService;

    public ReviewerTagDataReviewServiceTests()
    {
        _reviewerTagDataReviewService = new ReviewerTagDataReviewService(
            _reviewService.Object,
            _reviewerTagDataReviewRepository.Object);
    }

    [Fact]
    public async Task CreateReviewerTagDataReview_ThrowsIfReviewDoesNotExist()
    {
        var reviewerTagDataReview = new ReviewerTagDataReview { Status = 0, ReviewerId = Guid.NewGuid() };
        var reviewId = Guid.NewGuid();

        _reviewService.Setup(x => x.AnyTagDataReview(reviewId)).ThrowsAsync(new NotFoundException($"Invalid reviewId - {reviewId}."));

        await Assert.ThrowsAsync<NotFoundException>(() => _reviewerTagDataReviewService.CreateReviewerTagDataReview(reviewId, reviewerTagDataReview));
    }

    [Fact]
    public async Task CreateReviewerTagDataReview_RunsOkayWithCorrectInput()
    {
        var reviewerTagDataReview = new ReviewerTagDataReview { Status = ReviewStatusEnum.Reviewed, ReviewerId = Guid.NewGuid() };
        var reviewId = Guid.NewGuid();

        _reviewService.Setup(x => x.AnyTagDataReview(reviewId)).ReturnsAsync(true);

        _reviewerTagDataReviewRepository.Setup(x => x.CreateReviewerTagDataReview(reviewerTagDataReview)).ReturnsAsync(reviewerTagDataReview);

        var result = await _reviewerTagDataReviewService.CreateReviewerTagDataReview(reviewId, reviewerTagDataReview);

        Assert.NotNull(result);
        Assert.Equal(reviewerTagDataReview.Status, result.Status);
        Assert.Equal(reviewerTagDataReview.TagDataReviewId, reviewId);
        _reviewService.Verify(x => x.AnyTagDataReview(reviewId), Times.Once);
        _reviewerTagDataReviewRepository.Verify(x => x.CreateReviewerTagDataReview(reviewerTagDataReview), Times.Once);
    }
}
