using datasheetapi.Adapters;
using datasheetapi.Dtos;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace tests.Services;
public class RevisionContainerReviewServiceTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly Mock<IRevisionContainerReviewRepository> _reviewRepositoryMock;
    private readonly Mock<IRevisionContainerService> _revisionContainerServiceMock;
    private readonly RevisionContainerReviewService _reviewService;

    public RevisionContainerReviewServiceTests()
    {
        _loggerFactoryMock = new Mock<ILoggerFactory>();
        _reviewRepositoryMock = new Mock<IRevisionContainerReviewRepository>();
        _revisionContainerServiceMock = new Mock<IRevisionContainerService>();
        _reviewService = new RevisionContainerReviewService(_loggerFactoryMock.Object, _reviewRepositoryMock.Object, _revisionContainerServiceMock.Object);
    }

    [Fact]
    public async Task GetRevisionContainerReview_ReturnsNull_WhenReviewNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _reviewRepositoryMock.Setup(x => x.GetRevisionContainerReview(id)).ReturnsAsync((RevisionContainerReview?)null);

        // Act
        var result = await _reviewService.GetRevisionContainerReview(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetRevisionContainerReview_ReturnsReview_WhenReviewFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var review = new RevisionContainerReview { Id = id };
        _reviewRepositoryMock.Setup(x => x.GetRevisionContainerReview(id)).ReturnsAsync(review);

        // Act
        var result = await _reviewService.GetRevisionContainerReview(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(review, result);
    }

    [Fact]
    public async Task GetRevisionContainerReviewDto_ReturnsNull_WhenReviewNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _reviewRepositoryMock.Setup(x => x.GetRevisionContainerReview(id)).ReturnsAsync((RevisionContainerReview?)null);

        // Act
        var result = await _reviewService.GetRevisionContainerReviewDto(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetRevisionContainerReviewDto_ReturnsDto_WhenReviewFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var review = new RevisionContainerReview { Id = id };
        _reviewRepositoryMock.Setup(x => x.GetRevisionContainerReview(id)).ReturnsAsync(review);

        // Act
        var result = await _reviewService.GetRevisionContainerReviewDto(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(review.ToDtoOrNull()?.Id, result.Id);
    }

    [Fact]
    public async Task GetRevisionContainerReviews_ReturnsReviews()
    {
        // Arrange
        var reviews = new List<RevisionContainerReview> { new RevisionContainerReview(), new RevisionContainerReview() };
        _reviewRepositoryMock.Setup(x => x.GetRevisionContainerReviews()).ReturnsAsync(reviews);

        // Act
        var result = await _reviewService.GetRevisionContainerReviews();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviews, result);
    }

    [Fact]
    public async Task GetRevisionContainerReviewDtos_ReturnsDtos()
    {
        // Arrange
        var reviews = new List<RevisionContainerReview> { new RevisionContainerReview(), new RevisionContainerReview() };
        _reviewRepositoryMock.Setup(x => x.GetRevisionContainerReviews()).ReturnsAsync(reviews);

        // Act
        var result = await _reviewService.GetRevisionContainerReviewDtos();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviews.ToDto().Count, result.Count);
    }

    [Fact]
    public async Task GetRevisionContainerReviewForTag_ReturnsReview()
    {
        // Arrange
        var revisionId = Guid.NewGuid();
        var reviews = new RevisionContainerReview();
        _reviewRepositoryMock.Setup(x => x.GetRevisionContainerReviewForRevision(revisionId)).ReturnsAsync(reviews);

        // Act
        var result = await _reviewService.GetRevisionContainerReviewForTag(revisionId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviews, result);
    }

    [Fact]
    public async Task GetRevisionContainerReviewDtoForTag_ReturnsDto()
    {
        // Arrange
        var revisionId = Guid.NewGuid();
        var reviewId = Guid.NewGuid();
        var review = new RevisionContainerReview { Id = reviewId };
        _reviewRepositoryMock.Setup(x => x.GetRevisionContainerReviewForRevision(revisionId)).ReturnsAsync(review);

        // Act
        var result = await _reviewService.GetRevisionContainerReviewDtoForTag(revisionId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(review.Id, result.Id);
    }

    [Fact]
    public async Task CreateRevisionContainerReview_ThrowsException_WhenInvalidRevision()
    {
        // Arrange
        var review = new RevisionContainerReviewDto { RevisionContainerId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        _revisionContainerServiceMock.Setup(x => x.GetRevisionContainers()).ReturnsAsync(new List<RevisionContainer>());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _reviewService.CreateRevisionContainerReview(review, azureUniqueId));
    }

    [Fact]
    public async Task CreateRevisionContainerReview_ReturnsReviewDto_WhenValidInput()
    {
        // Arrange
        var azureUniqueId = Guid.NewGuid();
        var reviewDto = new RevisionContainerReviewDto
        {
            ApproverId = azureUniqueId,
            RevisionContainerId = Guid.NewGuid(),
        };
        var revisionContainer = new RevisionContainer
        {
            Id = reviewDto.RevisionContainerId
        };
        var reviewModel = new RevisionContainerReview
        {
            ApproverId = azureUniqueId,
            RevisionContainerId = Guid.NewGuid(),
        };
        var savedReview = new RevisionContainerReview
        {
            Id = Guid.NewGuid(),
            ApproverId = azureUniqueId,
            RevisionContainerId = Guid.NewGuid(),
        };

        _revisionContainerServiceMock.Setup(s => s.GetRevisionContainer(reviewDto.RevisionContainerId)).ReturnsAsync(revisionContainer);
        _reviewRepositoryMock.Setup(s => s.AddRevisionContainerReview(It.IsAny<RevisionContainerReview>())).ReturnsAsync(savedReview);

        // Act
        var result = await _reviewService.CreateRevisionContainerReview(reviewDto, azureUniqueId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(savedReview.Id, result.Id);
        Assert.Equal(azureUniqueId, result.ApproverId);
        _revisionContainerServiceMock.Verify(s => s.GetRevisionContainer(reviewDto.RevisionContainerId), Times.Once);
    }
}
