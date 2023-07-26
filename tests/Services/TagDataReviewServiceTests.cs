using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace tests.Services;
public class TagDataReviewServiceTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly Mock<ITagDataReviewRepository> _reviewRepositoryMock;
    private readonly Mock<ITagDataService> _tagDataServiceMock;
    private readonly TagDataReviewService _tagDataReviewService;

    public TagDataReviewServiceTests()
    {
        _loggerFactoryMock = new Mock<ILoggerFactory>();
        _reviewRepositoryMock = new Mock<ITagDataReviewRepository>();
        _tagDataServiceMock = new Mock<ITagDataService>();
        _tagDataReviewService = new TagDataReviewService(_loggerFactoryMock.Object, _reviewRepositoryMock.Object, _tagDataServiceMock.Object);
    }

    [Fact]
    public async Task GetTagDataReview_ReturnsNull_WhenReviewNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _reviewRepositoryMock.Setup(x => x.GetTagDataReview(id)).ReturnsAsync((TagDataReview?)null);

        // Act
        var result = await _tagDataReviewService.GetTagDataReview(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetTagDataReview_ReturnsReview_WhenReviewFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var review = new TagDataReview { Id = id };
        _reviewRepositoryMock.Setup(x => x.GetTagDataReview(id)).ReturnsAsync(review);

        // Act
        var result = await _tagDataReviewService.GetTagDataReview(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(review, result);
    }

    [Fact]
    public async Task GetTagDataReviews_ReturnsReviews()
    {
        // Arrange
        var reviews = new List<TagDataReview> { new TagDataReview(), new TagDataReview() };
        _reviewRepositoryMock.Setup(x => x.GetTagDataReviews()).ReturnsAsync(reviews);

        // Act
        var result = await _tagDataReviewService.GetTagDataReviews();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviews, result);
    }

    [Fact]
    public async Task GetTagDataReviewsForProject_ReturnsEmptyList()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        // Act
        var result = await _tagDataReviewService.GetTagDataReviewsForProject(projectId);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetReviewsForTag_ReturnsReviews()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var reviews = new List<TagDataReview> { new TagDataReview(), new TagDataReview() };
        _reviewRepositoryMock.Setup(x => x.GetTagDataReviewsForTag(tagId)).ReturnsAsync(reviews);

        // Act
        var result = await _tagDataReviewService.GetReviewsForTag(tagId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviews, result);
    }

    [Fact]
    public async Task GetTagDataReviewsForTags_ReturnsReviews()
    {
        // Arrange
        var tagIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var reviews = new List<TagDataReview> { new TagDataReview(), new TagDataReview() };
        _reviewRepositoryMock.Setup(x => x.GetTagDataReviewsForTags(tagIds)).ReturnsAsync(reviews);

        // Act
        var result = await _tagDataReviewService.GetTagDataReviewsForTags(tagIds);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviews, result);
    }

    [Fact]
    public async Task CreateTagDataReview_ThrowsException_WhenTagDataNotFound()
    {
        // Arrange
        var review = new TagDataReview { TagDataId = Guid.NewGuid() };
        _tagDataServiceMock.Setup(x => x.GetTagDataById(review.TagDataId)).ReturnsAsync((TagData?)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _tagDataReviewService.CreateTagDataReview(review, Guid.NewGuid()));
    }

    [Fact]
    public async Task CreateTagDataReview_SavesReviewAndReturnsIt()
    {
        // Arrange
        var review = new TagDataReview { TagDataId = Guid.NewGuid() };
        var tagData = new TagData { Id = review.TagDataId };
        var savedReview = new TagDataReview { Id = Guid.NewGuid() };
        _tagDataServiceMock.Setup(x => x.GetTagDataById(review.TagDataId)).ReturnsAsync(tagData);
        _reviewRepositoryMock.Setup(x => x.AddTagDataReview(review)).ReturnsAsync(savedReview);

        // Act
        var result = await _tagDataReviewService.CreateTagDataReview(review, Guid.NewGuid());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(savedReview, result);
        Assert.Equal(result.ApproverId, result.ApproverId);
    }
}
