using datasheetapi.Adapters;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace tests.Services;
public class CommentServiceTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock = new();
    private readonly Mock<ITagDataService> _tagDataServiceMock = new();
    private readonly Mock<ICommentRepository> _commentRepositoryMock = new();
    private readonly Mock<IAzureUserCacheService> _azureUserCacheServiceMock = new();
    private readonly Mock<IFusionService> _fusionServiceMock = new();
    private readonly Mock<ITagDataReviewService> _tagDataReviewServiceMock = new();
    private readonly Mock<IRevisionContainerReviewService> _revisionContainerReviewServiceMock = new();

    private readonly CommentService _commentService;

    public CommentServiceTests()
    {
        _commentService = new CommentService(
            _loggerFactoryMock.Object,
            _tagDataServiceMock.Object,
            _commentRepositoryMock.Object,
            _azureUserCacheServiceMock.Object,
            _fusionServiceMock.Object,
            _tagDataReviewServiceMock.Object,
            _revisionContainerReviewServiceMock.Object);
    }

    [Fact]
    public async Task GetComment_ReturnsCommentWithCommenterName()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var comment = new Comment { Id = commentId, UserId = Guid.NewGuid() };
        _commentRepositoryMock.Setup(x => x.GetComment(commentId)).ReturnsAsync(comment);
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(comment.UserId)).ReturnsAsync(new AzureUser { AzureUniqueId = comment.UserId, Name = "Test User" });

        // Act
        var result = await _commentService.GetComment(commentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(commentId, result.Id);
        Assert.Equal("Test User", result.CommenterName);
    }

    [Fact]
    public async Task GetComment_ReturnsNull_WhenCommentNotFound()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        _commentRepositoryMock.Setup(x => x.GetComment(commentId)).ReturnsAsync((Comment?)null);

        // Act
        var result = await _commentService.GetComment(commentId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetCommentDto_ReturnsCommentDto_WhenCommentFound()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var comment = new Comment { Id = commentId };
        _commentRepositoryMock.Setup(x => x.GetComment(commentId)).ReturnsAsync(comment);

        // Act
        var result = await _commentService.GetCommentDto(commentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(commentId, result.Id);
    }

    [Fact]
    public async Task GetCommentDto_ReturnsNull_WhenCommentNotFound()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        _commentRepositoryMock.Setup(x => x.GetComment(commentId)).ReturnsAsync((Comment?)null);

        // Act
        var result = await _commentService.GetCommentDto(commentId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetComments_ReturnsCommentsWithCommenterNames()
    {
        // Arrange
        var comments = new List<Comment>
        {
            new Comment { Id = Guid.NewGuid(), UserId = Guid.NewGuid() },
            new Comment { Id = Guid.NewGuid(), UserId = Guid.NewGuid() }
        };
        _commentRepositoryMock.Setup(x => x.GetComments()).ReturnsAsync(comments);
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(It.IsAny<Guid>())).ReturnsAsync(new AzureUser { Name = "Test User" });

        // Act
        var result = await _commentService.GetComments();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comments.Count, result.Count);
        foreach (var comment in result)
        {
            Assert.NotNull(comment.CommenterName);
        }
    }

    [Fact]
    public async Task GetCommentDtos_ReturnsCommentDtos_WhenCommentsFound()
    {
        // Arrange
        var comments = new List<Comment>
        {
            new Comment { Id = Guid.NewGuid() },
            new Comment { Id = Guid.NewGuid() }
        };
        _commentRepositoryMock.Setup(x => x.GetComments()).ReturnsAsync(comments);

        // Act
        var result = await _commentService.GetCommentDtos();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comments.Count, result.Count);
        foreach (var comment in result)
        {
            Assert.NotNull(comment);
            Assert.NotEqual(Guid.Empty, comment.Id);
        }
    }

    [Fact]
    public async Task GetCommentsForTagReview_ReturnsCommentsWithCommenterNames()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var comments = new List<Comment>
        {
            new Comment { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), TagDataReviewId = tagId },
            new Comment { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), TagDataReviewId = tagId }
        };
        _commentRepositoryMock.Setup(x => x.GetCommentsForTagReview(tagId)).ReturnsAsync(comments);
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(It.IsAny<Guid>())).ReturnsAsync(new AzureUser { Name = "Test User" });

        // Act
        var result = await _commentService.GetCommentsForTagReview(tagId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comments.Count, result.Count);
        foreach (var comment in result)
        {
            Assert.NotNull(comment.CommenterName);
        }
    }

    [Fact]
    public async Task GetCommentDtosForTagReview_ReturnsCommentDtos_WhenCommentsFound()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var comments = new List<Comment>
        {
            new Comment { Id = Guid.NewGuid(), TagDataReviewId = tagId },
            new Comment { Id = Guid.NewGuid(), TagDataReviewId = tagId }
        };
        _commentRepositoryMock.Setup(x => x.GetCommentsForTagReview(tagId)).ReturnsAsync(comments);

        // Act
        var result = await _commentService.GetCommentDtosForTagReview(tagId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comments.Count, result.Count);
        foreach (var comment in result)
        {
            Assert.NotNull(comment);
            Assert.NotEqual(Guid.Empty, comment.Id);
        }
    }

    [Fact]
    public async Task GetCommentsForTagReviews_ReturnsCommentsWithCommenterNames()
    {
        // Arrange
        var tagIds = new List<Guid?> { Guid.NewGuid(), Guid.NewGuid() };
        var comments = new List<Comment>
        {
            new Comment { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), TagDataReviewId = tagIds[0] },
            new Comment { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), TagDataReviewId = tagIds[1] }
        };
        _commentRepositoryMock.Setup(x => x.GetCommentsForTagReviews(tagIds)).ReturnsAsync(comments);
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(It.IsAny<Guid>())).ReturnsAsync(new AzureUser { Name = "Test User" });

        // Act
        var result = await _commentService.GetCommentsForTagReviews(tagIds);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comments.Count, result.Count);
        foreach (var comment in result)
        {
            Assert.NotNull(comment.CommenterName);
        }
    }

    [Fact]
    public async Task CreateTagDataReviewComment_ThrowsException_WhenInvalidTagDataReviewId()
    {
        // Arrange
        var comment = new Comment { TagDataReviewId = null };
        var azureUniqueId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _commentService.CreateTagDataReviewComment(comment, azureUniqueId));
    }

    [Fact]
    public async Task CreateTagDataReviewComment_ThrowsException_WhenInvalidTagDataReview()
    {
        // Arrange
        var comment = new Comment { TagDataReviewId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        _tagDataReviewServiceMock.Setup(x => x.GetTagDataReview(comment.TagDataReviewId.Value)).ReturnsAsync((TagDataReview?)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _commentService.CreateTagDataReviewComment(comment, azureUniqueId));
    }

    [Fact]
    public async Task CreateTagDataReviewComment_CallsCreateComment_WithCorrectParameters()
    {
        // Arrange
        var comment = new Comment { TagDataReviewId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        var tagDataReview = new TagDataReview { Id = comment.TagDataReviewId.Value };
        _tagDataReviewServiceMock.Setup(x => x.GetTagDataReview(comment.TagDataReviewId.Value)).ReturnsAsync(tagDataReview);
        _commentRepositoryMock.Setup(x => x.AddComment(It.IsAny<Comment>())).ReturnsAsync(comment);

        // Act
        var result = await _commentService.CreateTagDataReviewComment(comment, azureUniqueId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comment.Id, result.Id);
        _commentRepositoryMock.Verify(x => x.AddComment(comment), Times.Once);
    }

    [Fact]
    public async Task CreateRevisionContainerReviewComment_ThrowsException_WhenInvalidRevisionContainerReviewId()
    {
        // Arrange
        var comment = new Comment { RevisionContainerReviewId = null };
        var azureUniqueId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _commentService.CreateRevisionContainerReviewComment(comment, azureUniqueId));
    }

    [Fact]
    public async Task CreateRevisionContainerReviewComment_ThrowsException_WhenInvalidRevisionContainerReview()
    {
        // Arrange
        var comment = new Comment { RevisionContainerReviewId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        _revisionContainerReviewServiceMock.Setup(x => x.GetRevisionContainerReview(comment.RevisionContainerReviewId.Value)).ReturnsAsync((RevisionContainerReview?)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _commentService.CreateRevisionContainerReviewComment(comment, azureUniqueId));
    }

    [Fact]
    public async Task CreateRevisionContainerReviewComment_CallsCreateComment_WithCorrectParameters()
    {
        // Arrange
        var comment = new Comment { RevisionContainerReviewId = Guid.NewGuid() };
        var azureUniqueId = Guid.NewGuid();
        var revisionContainerReview = new RevisionContainerReview { Id = comment.RevisionContainerReviewId.Value };
        _revisionContainerReviewServiceMock.Setup(x => x.GetRevisionContainerReview(comment.RevisionContainerReviewId.Value)).ReturnsAsync(revisionContainerReview);
        _commentRepositoryMock.Setup(x => x.AddComment(It.IsAny<Comment>())).ReturnsAsync(comment);

        // Act
        var result = await _commentService.CreateRevisionContainerReviewComment(comment, azureUniqueId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comment.Id, result.Id);
        _commentRepositoryMock.Verify(x => x.AddComment(comment), Times.Once);
    }
}
