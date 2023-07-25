using datasheetapi.Adapters;
using datasheetapi.Dtos;
using datasheetapi.Models;

namespace tests.Adapters;
public class RevisionContainerReviewAdapterTests
{
    [Fact]
    public void ToDtoOrNull_WithNullRevisionContainerReview_ReturnsNull()
    {
        // Arrange
        RevisionContainerReview? revisionContainerReview = null;

        // Act
        var result = revisionContainerReview.ToDtoOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToDtoOrNull_WithNonNullRevisionContainerReview_ReturnsRevisionContainerReviewDto()
    {
        // Arrange
        var revisionContainerReview = new RevisionContainerReview
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            Status = ReviewStatusEnum.New,
            ApproverId = Guid.NewGuid(),
            CommentResponsible = new Guid(),
            Approved = false,
            RevisionContainerVersion = 1,
            RevisionContainerId = Guid.NewGuid(),
            Comments = new List<Comment>(),
        };

        // Act
        var result = revisionContainerReview.ToDtoOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainerReview.Id, result.Id);
        Assert.Equal(revisionContainerReview.CreatedDate, result.CreatedDate);
        Assert.Equal(revisionContainerReview.ModifiedDate, result.ModifiedDate);
        Assert.Equal(revisionContainerReview.Status, result.Status);
        Assert.Equal(revisionContainerReview.ApproverId, result.ApproverId);
        Assert.Equal(revisionContainerReview.CommentResponsible, result.CommentResponsible);
        Assert.Equal(revisionContainerReview.Approved, result.Approved);
        Assert.Equal(revisionContainerReview.RevisionContainerVersion, result.RevisionContainerVersion);
        Assert.Equal(revisionContainerReview.RevisionContainerId, result.RevisionContainerId);
        Assert.NotNull(result.Comments);
        Assert.Empty(result.Comments);
    }

    [Fact]
    public void ToDto_WithNullRevisionContainerReviews_ReturnsEmptyList()
    {
        // Arrange
        List<RevisionContainerReview>? revisionContainerReviews = null;

        // Act
        var result = revisionContainerReviews.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToDto_WithNonNullRevisionContainerReviews_ReturnsListOfRevisionContainerReviewDtos()
    {
        // Arrange
        var revisionContainerReviews = new List<RevisionContainerReview>
        {
            new RevisionContainerReview
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
            Status = ReviewStatusEnum.New,
                ApproverId = Guid.NewGuid(),
            CommentResponsible = new Guid(),
                Approved = false,
                RevisionContainerVersion = 1,
                RevisionContainerId = Guid.NewGuid(),
                Comments = new List<Comment>(),
            },
            new RevisionContainerReview
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
            Status = ReviewStatusEnum.New,
                ApproverId = Guid.NewGuid(),
            CommentResponsible = new Guid(),
                Approved = true,
                RevisionContainerVersion = 2,
                RevisionContainerId = Guid.NewGuid(),
                Comments = new List<Comment>(),
            },
        };

        // Act
        var result = revisionContainerReviews.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainerReviews.Count, result.Count);
        for (int i = 0; i < revisionContainerReviews.Count; i++)
        {
            Assert.Equal(revisionContainerReviews[i].Id, result[i].Id);
            Assert.Equal(revisionContainerReviews[i].CreatedDate, result[i].CreatedDate);
            Assert.Equal(revisionContainerReviews[i].ModifiedDate, result[i].ModifiedDate);
            Assert.Equal(revisionContainerReviews[i].Status, result[i].Status);
            Assert.Equal(revisionContainerReviews[i].ApproverId, result[i].ApproverId);
            Assert.Equal(revisionContainerReviews[i].CommentResponsible, result[i].CommentResponsible);
            Assert.Equal(revisionContainerReviews[i].Approved, result[i].Approved);
            Assert.Equal(revisionContainerReviews[i].RevisionContainerVersion, result[i].RevisionContainerVersion);
            Assert.Equal(revisionContainerReviews[i].RevisionContainerId, result[i].RevisionContainerId);
            Assert.NotNull(result[i].Comments);
            Assert.Empty(result[i].Comments);
        }
    }

    [Fact]
    public void ToModelOrNull_WithNullRevisionContainerReviewDto_ReturnsNull()
    {
        // Arrange
        RevisionContainerReviewDto? revisionContainerReviewDto = null;

        // Act
        var result = revisionContainerReviewDto.ToModelOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToModelOrNull_WithNonNullRevisionContainerReviewDto_ReturnsRevisionContainerReview()
    {
        // Arrange
        var revisionContainerReviewDto = new RevisionContainerReviewDto
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            Status = ReviewStatusEnum.New,
            ApproverId = Guid.NewGuid(),
            CommentResponsible = new Guid(),
            Approved = false,
            RevisionContainerVersion = 1,
            RevisionContainerId = Guid.NewGuid(),
            Comments = new List<CommentDto>(),
        };

        // Act
        var result = revisionContainerReviewDto.ToModelOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainerReviewDto.Id, result.Id);
        Assert.Equal(revisionContainerReviewDto.CreatedDate, result.CreatedDate);
        Assert.Equal(revisionContainerReviewDto.ModifiedDate, result.ModifiedDate);
        Assert.Equal(revisionContainerReviewDto.Status, result.Status);
        Assert.Equal(revisionContainerReviewDto.ApproverId, result.ApproverId);
        Assert.Equal(revisionContainerReviewDto.CommentResponsible, result.CommentResponsible);
        Assert.Equal(revisionContainerReviewDto.Approved, result.Approved);
        Assert.Equal(revisionContainerReviewDto.RevisionContainerVersion, result.RevisionContainerVersion);
        Assert.Equal(revisionContainerReviewDto.RevisionContainerId, result.RevisionContainerId);
        Assert.NotNull(result.Comments);
        Assert.Empty(result.Comments);
    }

    [Fact]
    public void ToModel_WithNullRevisionContainerReviewDtos_ReturnsEmptyList()
    {
        // Arrange
        List<RevisionContainerReviewDto>? revisionContainerReviewDtos = null;

        // Act
        var result = revisionContainerReviewDtos.ToModel();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToModel_WithNonNullRevisionContainerReviewDtos_ReturnsListOfRevisionContainerReviews()
    {
        // Arrange
        var revisionContainerReviewDtos = new List<RevisionContainerReviewDto>
        {
            new RevisionContainerReviewDto
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
            Status = ReviewStatusEnum.New,
                ApproverId = Guid.NewGuid(),
            CommentResponsible = new Guid(),
                Approved = false,
                RevisionContainerVersion = 1,
                RevisionContainerId = Guid.NewGuid(),
                Comments = new List<CommentDto>(),
            },
            new RevisionContainerReviewDto
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
            Status = ReviewStatusEnum.New,
                ApproverId = Guid.NewGuid(),
            CommentResponsible = new Guid(),
                Approved = true,
                RevisionContainerVersion = 2,
                RevisionContainerId = Guid.NewGuid(),
                Comments = new List<CommentDto>(),
            },
        };

        // Act
        var result = revisionContainerReviewDtos.ToModel();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainerReviewDtos.Count, result.Count);
        for (int i = 0; i < revisionContainerReviewDtos.Count; i++)
        {
            Assert.Equal(revisionContainerReviewDtos[i].Id, result[i].Id);
            Assert.Equal(revisionContainerReviewDtos[i].CreatedDate, result[i].CreatedDate);
            Assert.Equal(revisionContainerReviewDtos[i].ModifiedDate, result[i].ModifiedDate);
            Assert.Equal(revisionContainerReviewDtos[i].Status, result[i].Status);
            Assert.Equal(revisionContainerReviewDtos[i].ApproverId, result[i].ApproverId);
            Assert.Equal(revisionContainerReviewDtos[i].CommentResponsible, result[i].CommentResponsible);
            Assert.Equal(revisionContainerReviewDtos[i].Approved, result[i].Approved);
            Assert.Equal(revisionContainerReviewDtos[i].RevisionContainerVersion, result[i].RevisionContainerVersion);
            Assert.Equal(revisionContainerReviewDtos[i].RevisionContainerId, result[i].RevisionContainerId);
            Assert.NotNull(result[i].Comments);
            Assert.Empty(result[i].Comments);
        }
    }
}
