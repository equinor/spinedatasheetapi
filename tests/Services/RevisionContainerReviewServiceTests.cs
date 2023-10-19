using datasheetapi.Adapters;
using datasheetapi.Dtos;
using datasheetapi.Exceptions;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace tests.Services;
public class RevisionContainerReviewServiceTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly Mock<IContainerReviewRepository> _reviewRepositoryMock;
    private readonly Mock<IContainerService> _revisionContainerServiceMock;
    private readonly ContainerReviewService _reviewService;

    public RevisionContainerReviewServiceTests()
    {
        _loggerFactoryMock = new Mock<ILoggerFactory>();
        _reviewRepositoryMock = new Mock<IContainerReviewRepository>();
        _revisionContainerServiceMock = new Mock<IContainerService>();
        _reviewService = new ContainerReviewService(_loggerFactoryMock.Object, _reviewRepositoryMock.Object, _revisionContainerServiceMock.Object);
    }

    [Fact]
    public async Task GetRevisionContainerReview_ReturnsNull_WhenReviewNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _reviewRepositoryMock.Setup(x => x.GetContainerReview(id)).ReturnsAsync((ContainerReview?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _reviewService.GetContainerReview(id));
    }

    [Fact]
    public async Task GetRevisionContainerReview_ReturnsReview_WhenReviewFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var review = new ContainerReview { Id = id };
        _reviewRepositoryMock.Setup(x => x.GetContainerReview(id)).ReturnsAsync(review);

        // Act
        var result = await _reviewService.GetContainerReview(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(review, result);
    }

    [Fact]
    public async Task GetRevisionContainerReviews_ReturnsReviews()
    {
        // Arrange
        var reviews = new List<ContainerReview> { new ContainerReview(), new ContainerReview() };
        _reviewRepositoryMock.Setup(x => x.GetContainerReviews()).ReturnsAsync(reviews);

        // Act
        var result = await _reviewService.GetContainerReviews();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviews, result);
    }

    [Fact]
    public async Task CreateRevisionContainerReview_ThrowsException_WhenInvalidRevision()
    {
        // Arrange
        var review = new ContainerReviewDto { ContainerId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        _revisionContainerServiceMock.Setup(x => x.GetContainers()).ReturnsAsync(new List<Container>());

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() =>
        _reviewService.CreateContainerReview(review.ToModel(), azureUniqueId));
    }

    [Fact]
    public async Task CreateRevisionContainerReview_ReturnsReviewDto_WhenValidInput()
    {
        // Arrange
        var azureUniqueId = Guid.NewGuid();
        var reviewDto = new ContainerReviewDto
        {
            ApproverId = azureUniqueId,
            ContainerId = Guid.NewGuid(),
        };
        var revisionContainer = new Container
        {
            Id = reviewDto.ContainerId
        };
        var reviewModel = new ContainerReview
        {
            ApproverId = azureUniqueId,
            ContainerId = Guid.NewGuid(),
        };
        var savedReview = new ContainerReview
        {
            Id = Guid.NewGuid(),
            ApproverId = azureUniqueId,
            ContainerId = Guid.NewGuid(),
        };

        _revisionContainerServiceMock.Setup(s => s.GetContainer(reviewDto.ContainerId)).ReturnsAsync(revisionContainer);
        _reviewRepositoryMock.Setup(s => s.AddContainerReview(It.IsAny<ContainerReview>())).ReturnsAsync(savedReview);

        // Act
        var result = await _reviewService.CreateContainerReview(reviewDto.ToModel(), azureUniqueId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(savedReview.Id, result.Id);
        Assert.Equal(azureUniqueId, result.ApproverId);
        _revisionContainerServiceMock.Verify(s => s.GetContainer(reviewDto.ContainerId), Times.Once);
    }
}
