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
        ContainerReview? revisionContainerReview = null;

        // Act
        var result = revisionContainerReview.ToDtoOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToDtoOrNull_WithNonNullRevisionContainerReview_ReturnsRevisionContainerReviewDto()
    {
        // Arrange
        var revisionContainerReview = GetRevisionContainerReview(1);

        // Act
        var result = revisionContainerReview.ToDtoOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainerReview.Id, result.Id);
        Assert.Equal(revisionContainerReview.CreatedDate, result.CreatedDate);
        Assert.Equal(revisionContainerReview.ModifiedDate, result.ModifiedDate);
        Assert.Equal(revisionContainerReview.Status.MapReviewStatusModelToDto(), result.Status);
        Assert.Equal(revisionContainerReview.ApproverId, result.ApproverId);
        Assert.Equal(revisionContainerReview.CommentResponsible, result.CommentResponsible);
        Assert.Equal(revisionContainerReview.Approved, result.Approved);
        Assert.Equal(revisionContainerReview.RevisionContainerVersion, result.ContainerVersion);
        Assert.Equal(revisionContainerReview.ContainerId, result.ContainerId);
    }

    [Fact]
    public void ToDto_WithNullRevisionContainerReviews_ReturnsEmptyList()
    {
        // Arrange
        List<ContainerReview>? revisionContainerReviews = null;

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
        var revisionContainerReviews = new List<ContainerReview>
                    { GetRevisionContainerReview(1), GetRevisionContainerReview(2),};

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
            Assert.Equal(revisionContainerReviews[i].Status.MapReviewStatusModelToDto(), result[i].Status);
            Assert.Equal(revisionContainerReviews[i].ApproverId, result[i].ApproverId);
            Assert.Equal(revisionContainerReviews[i].CommentResponsible, result[i].CommentResponsible);
            Assert.Equal(revisionContainerReviews[i].Approved, result[i].Approved);
            Assert.Equal(revisionContainerReviews[i].RevisionContainerVersion, result[i].ContainerVersion);
            Assert.Equal(revisionContainerReviews[i].ContainerId, result[i].ContainerId);
        }
    }

    [Fact]
    public void ToModelOrNull_WithNullRevisionContainerReviewDto_ReturnsNull()
    {
        // Arrange
        ContainerReviewDto? revisionContainerReviewDto = null;

        // Act
        var result = revisionContainerReviewDto.ToModelOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToModelOrNull_WithNonNullRevisionContainerReviewDto_ReturnsRevisionContainerReview()
    {
        ContainerReviewDto revisionContainerReviewDto = GetContainerReviewDto(1);

        // Act
        var result = revisionContainerReviewDto.ToModelOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainerReviewDto.Id, result.Id);
        Assert.Equal(revisionContainerReviewDto.CreatedDate, result.CreatedDate);
        Assert.Equal(revisionContainerReviewDto.ModifiedDate, result.ModifiedDate);
        Assert.Equal(revisionContainerReviewDto.Status.MapReviewStatusDtoToModel(), result.Status);
        Assert.Equal(revisionContainerReviewDto.ApproverId, result.ApproverId);
        Assert.Equal(revisionContainerReviewDto.CommentResponsible, result.CommentResponsible);
        Assert.Equal(revisionContainerReviewDto.Approved, result.Approved);
        Assert.Equal(revisionContainerReviewDto.ContainerVersion, result.RevisionContainerVersion);
        Assert.Equal(revisionContainerReviewDto.ContainerId, result.ContainerId);
        Assert.NotNull(result.Conversations);
        Assert.Empty(result.Conversations);
    }

    [Fact]
    public void ToModel_WithNonNullCreateContainerReviewDtos_ReturnsListOfRevisionContainerReviews()
    {
        // Arrange
        var revisionContainerReviewDto = new CreateContainerReviewDto()
        {
            RevisionContainerId = Guid.NewGuid(),
            Status = ReviewStatusDto.New
        };

        // Act
        var result = revisionContainerReviewDto.ToModel();

        // Assert
        Assert.NotNull(result);

        Assert.Equal(revisionContainerReviewDto.RevisionContainerId, result.ContainerId);
        Assert.Equal(revisionContainerReviewDto.Status.MapReviewStatusDtoToModel(), result.Status);
    }

    private static ContainerReview GetRevisionContainerReview(int version)
    {
        return new ContainerReview
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            Status = ReviewStateEnum.New,
            ApproverId = Guid.NewGuid(),
            CommentResponsible = new Guid(),
            Approved = false,
            RevisionContainerVersion = version,
            ContainerId = Guid.NewGuid(),
            Conversations = new List<Conversation>(),
        };
    }

    private static ContainerReviewDto GetContainerReviewDto(int version)
    {
        // Arrange
        return new ContainerReviewDto
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            Status = ReviewStatusDto.New,
            ApproverId = Guid.NewGuid(),
            CommentResponsible = new Guid(),
            Approved = false,
            ContainerVersion = version,
            ContainerId = Guid.NewGuid(),
        };
    }
}
