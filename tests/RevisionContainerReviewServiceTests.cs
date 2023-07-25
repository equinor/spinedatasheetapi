using datasheetapi.Dtos;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;
using datasheetapi.Adapters;

using Microsoft.Extensions.Logging;

using Moq;

namespace tests;
public class RevisionContainerReviewServiceTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly Mock<IRevisionContainerReviewRepository> _reviewRepositoryMock;
    private readonly Mock<ITagDataService> _tagDataServiceMock;
    private readonly RevisionContainerReviewService _reviewService;

    public RevisionContainerReviewServiceTests()
    {
        _loggerFactoryMock = new Mock<ILoggerFactory>();
        _reviewRepositoryMock = new Mock<IRevisionContainerReviewRepository>();
        _tagDataServiceMock = new Mock<ITagDataService>();
        _reviewService = new RevisionContainerReviewService(_loggerFactoryMock.Object, _reviewRepositoryMock.Object, _tagDataServiceMock.Object);
    }

    [Fact]
    public async Task GetRevisionContainerReview_ReturnsNull_WhenReviewNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _reviewRepositoryMock.Setup(x => x.GetTagDataReview(id)).ReturnsAsync((RevisionContainerReview?)null);

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
        _reviewRepositoryMock.Setup(x => x.GetTagDataReview(id)).ReturnsAsync(review);

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
        _reviewRepositoryMock.Setup(x => x.GetTagDataReview(id)).ReturnsAsync((RevisionContainerReview?)null);

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
        _reviewRepositoryMock.Setup(x => x.GetTagDataReview(id)).ReturnsAsync(review);

        // Act
        var result = await _reviewService.GetRevisionContainerReviewDto(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(review.ToDtoOrNull().Id, result.Id);
    }

    [Fact]
    public async Task GetRevisionContainerReviews_ReturnsReviews()
    {
        // Arrange
        var reviews = new List<RevisionContainerReview> { new RevisionContainerReview(), new RevisionContainerReview() };
        _reviewRepositoryMock.Setup(x => x.GetTagDataReviews()).ReturnsAsync(reviews);

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
        _reviewRepositoryMock.Setup(x => x.GetTagDataReviews()).ReturnsAsync(reviews);

        // Act
        var result = await _reviewService.GetRevisionContainerReviewDtos();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviews.ToDto().Count, result.Count);
    }

    [Fact]
    public async Task GetRevisionContainerReviewsForTag_ReturnsReviews()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var reviews = new List<RevisionContainerReview> { new RevisionContainerReview(), new RevisionContainerReview() };
        _reviewRepositoryMock.Setup(x => x.GetRevisionContainerReviewForRevision(tagId)).ReturnsAsync(reviews);

        // Act
        var result = await _reviewService.GetRevisionContainerReviewsForTag(tagId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviews, result);
    }

    [Fact]
    public async Task GetRevisionContainerReviewDtosForTag_ReturnsDtos()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var reviews = new List<RevisionContainerReview> { new RevisionContainerReview(), new RevisionContainerReview() };
        _reviewRepositoryMock.Setup(x => x.GetRevisionContainerReviewForRevision(tagId)).ReturnsAsync(reviews);

        // Act
        var result = await _reviewService.GetRevisionContainerReviewDtosForTag(tagId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviews.ToDto().Count, result.Count);
    }

    [Fact]
    public async Task CreateRevisionContainerReview_ThrowsException_WhenInvalidReview()
    {
        // Arrange
        var review = new RevisionContainerReviewDto { RevisionContainerId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        var tagData = new List<ITagData> { new TagData { RevisionContainer = new RevisionContainer { Id = Guid.NewGuid() } } };
        var reviewModel = new RevisionContainerReview();
        var savedReview = new RevisionContainerReview();
        _tagDataServiceMock.Setup(x => x.GetAllTagData()).ReturnsAsync(tagData);
        _reviewRepositoryMock.Setup(x => x.AddTagDataReview(reviewModel)).ReturnsAsync(savedReview);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _reviewService.CreateRevisionContainerReview(review, azureUniqueId));
    }

    [Fact]
    public async Task CreateRevisionContainerReview_ThrowsException_WhenInvalidRevision()
    {
        // Arrange
        var review = new RevisionContainerReviewDto { RevisionContainerId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        _tagDataServiceMock.Setup(x => x.GetAllTagData()).ReturnsAsync(new List<ITagData>());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _reviewService.CreateRevisionContainerReview(review, azureUniqueId));
    }

    [Fact]
    public async Task CreateRevisionContainerReview_ThrowsException_WhenSavingReviewFails()
    {
        // Arrange
        var review = new RevisionContainerReviewDto { RevisionContainerId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        var tagData = new List<ITagData> { new TagData { RevisionContainer = new RevisionContainer { Id = review.RevisionContainerId } } };
        _tagDataServiceMock.Setup(x => x.GetAllTagData()).ReturnsAsync(tagData);
        _reviewRepositoryMock.Setup(x => x.AddTagDataReview(It.IsAny<RevisionContainerReview>())).ReturnsAsync((RevisionContainerReview?)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _reviewService.CreateRevisionContainerReview(review, azureUniqueId));
    }

    [Fact]
    public async Task CreateRevisionContainerReview_ReturnsReviewDto_WhenValidInput()
    {
        // Arrange
        var review = new RevisionContainerReviewDto { RevisionContainerId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        review.ApproverId = azureUniqueId;
        var tagData = new List<ITagData> { new TagData { RevisionContainer = new RevisionContainer { Id = review.RevisionContainerId } } };
        var savedReview = new RevisionContainerReview { Id = Guid.NewGuid(), RevisionContainerId = review.RevisionContainerId };
        _tagDataServiceMock.Setup(x => x.GetAllTagData()).ReturnsAsync(tagData);
        _reviewRepositoryMock.Setup(x => x.AddTagDataReview(It.IsAny<RevisionContainerReview>())).ReturnsAsync(savedReview);

        // Act
        var result = await _reviewService.CreateRevisionContainerReview(review, azureUniqueId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(savedReview.ToDtoOrNull().Id, result.Id);
    }

    [Fact]
    public async Task CreateRevisionContainerReview_ThrowsException_WhenAddTagDataReviewReturnsNull()
    {
        // Arrange
        var review = new RevisionContainerReviewDto { RevisionContainerId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        var tagData = new List<ITagData> { new TagData { RevisionContainer = new RevisionContainer { Id = review.RevisionContainerId } } };
        var reviewModel = new RevisionContainerReview();
        _tagDataServiceMock.Setup(x => x.GetAllTagData()).ReturnsAsync(tagData);
        _reviewRepositoryMock.Setup(x => x.AddTagDataReview(reviewModel)).ReturnsAsync((RevisionContainerReview?)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _reviewService.CreateRevisionContainerReview(review, azureUniqueId));
    }

    [Fact]
    public async Task CreateRevisionContainerReview_ThrowsException_WhenToDtoOrNullReturnsNull()
    {
        // Arrange
        var review = new RevisionContainerReviewDto { RevisionContainerId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        var tagData = new List<ITagData> { new TagData { RevisionContainer = new RevisionContainer { Id = review.RevisionContainerId } } };
        var reviewModel = new RevisionContainerReview();
        var savedReview = new RevisionContainerReview();
        _tagDataServiceMock.Setup(x => x.GetAllTagData()).ReturnsAsync(tagData);
        _reviewRepositoryMock.Setup(x => x.AddTagDataReview(reviewModel)).ReturnsAsync(savedReview);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _reviewService.CreateRevisionContainerReview(review, azureUniqueId));
    }
}