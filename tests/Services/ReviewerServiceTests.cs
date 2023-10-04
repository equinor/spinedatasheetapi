using datasheetapi.Exceptions;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Moq;

namespace tests.Services;
public class ReviewerServiceTests
{
    private readonly Mock<ITagDataReviewService> _reviewService = new();
    private readonly Mock<IReviewerRepository> _reviewerRepository = new();

    private readonly ReviewerService _reviewerService;

    public ReviewerServiceTests()
    {
        _reviewerService = new ReviewerService(
            _reviewService.Object,
            _reviewerRepository.Object);
    }

    [Fact]
    public async Task CreateReviewerTagDataReview_ThrowsIfReviewDoesNotExist()
    {
        List<Reviewer> reviewers = new()
        {
            new Reviewer { Status = 0, ReviewerId = Guid.NewGuid() }
        };
        var reviewId = Guid.NewGuid();

        _reviewService.Setup(x => x.AnyTagDataReview(reviewId)).ThrowsAsync(new NotFoundException($"Invalid reviewId - {reviewId}."));

        await Assert.ThrowsAsync<NotFoundException>(() => _reviewerService.CreateReviewers(reviewId, reviewers));
    }

    [Fact]
    public async Task CreateReviewerTagDataReview_RunsOkayWithCorrectInput()
    {
        List<Reviewer> reviewers = new()
        {
            new Reviewer { Status = 0, ReviewerId = Guid.NewGuid() },
            new Reviewer { Status = 0, ReviewerId = Guid.NewGuid() }
        };

        var reviewId = Guid.NewGuid();

        _reviewService.Setup(x => x.AnyTagDataReview(reviewId)).ReturnsAsync(true);

        _reviewerRepository.Setup(x => x.CreateReviewers(reviewers)).ReturnsAsync(reviewers);

        var result = await _reviewerService.CreateReviewers(reviewId, reviewers);

        Assert.NotNull(result);
        Assert.Equal(reviewers, result);
        Assert.Equal(reviewers[0].Status, result[0].Status);
        Assert.Equal(reviewers[0].TagDataReviewId, reviewId);
        _reviewService.Verify(x => x.AnyTagDataReview(reviewId), Times.Once);
        _reviewerRepository.Verify(x => x.CreateReviewers(reviewers), Times.Once);
    }
}
