using datasheetapi.Adapters;
using datasheetapi.Dtos;
using datasheetapi.Models;

namespace tests.Adapters;

public class TagDataReviewAdapterTests
{
    [Fact]
    public void ToDtoOrNull_WithNullTagDataReview_ReturnsNull()
    {
        // Arrange
        TagDataReview? tagDataReview = null;

        // Act
        var result = tagDataReview.ToDtoOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToDtoOrNull_WithNonNullTagDataReview_ReturnsTagDataReviewDto()
    {
        TagDataReview tagDataReview = GetTagDataReview("Tag-002", 1);

        // Act
        var result = tagDataReview.ToDtoOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tagDataReview.Id, result.Id);
        Assert.Equal(tagDataReview.CreatedDate, result.CreatedDate);
        Assert.Equal(tagDataReview.ModifiedDate, result.ModifiedDate);
        Assert.Equal(tagDataReview.TagNo, result.TagNo);
        Assert.Equal(tagDataReview.Status, result.Status);
        Assert.Equal(tagDataReview.ApproverId, result.ApproverId);
        Assert.Equal(tagDataReview.CommentResponsible, result.CommentResponsible);
        Assert.Equal(tagDataReview.Approved, result.Approved);
        Assert.Equal(tagDataReview.TagDataVersion, result.TagDataVersion);
    }

    [Fact]
    public void ToDto_WithNullTagDataReviews_ReturnsEmptyList()
    {
        // Arrange
        List<TagDataReview>? tagDataReviews = null;

        // Act
        var result = tagDataReviews.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToDto_WithNonNullTagDataReviews_ReturnsListOfTagDataReviewDtos()
    {
        // Arrange
        var tagDataReviews = new List<TagDataReview>
                { GetTagDataReview("Tag-003", 1), GetTagDataReview("Tag-004", 2) };

        // Act
        var result = tagDataReviews.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tagDataReviews.Count, result.Count);
        for (int i = 0; i < tagDataReviews.Count; i++)
        {
            Assert.Equal(tagDataReviews[i].Id, result[i].Id);
            Assert.Equal(tagDataReviews[i].CreatedDate, result[i].CreatedDate);
            Assert.Equal(tagDataReviews[i].ModifiedDate, result[i].ModifiedDate);
            Assert.Equal(tagDataReviews[i].TagNo, result[i].TagNo);
            Assert.Equal(tagDataReviews[i].Status, result[i].Status);
            Assert.Equal(tagDataReviews[i].ApproverId, result[i].ApproverId);
            Assert.Equal(tagDataReviews[i].CommentResponsible, result[i].CommentResponsible);
            Assert.Equal(tagDataReviews[i].Approved, result[i].Approved);
            Assert.Equal(tagDataReviews[i].TagDataVersion, result[i].TagDataVersion);
        }
    }

    [Fact]
    public void ToModelOrNull_WithNullTagDataReviewDto_ReturnsNull()
    {
        // Arrange
        TagDataReviewDto? tagDataReviewDto = null;

        // Act
        var result = tagDataReviewDto.ToModelOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToModelOrNull_WithNonNullTagDataReviewDto_ReturnsTagDataReview()
    {
        // Arrange
        var tagDataReviewDto = GetTagDataReviewDto("Tag-005");

        // Act
        var result = tagDataReviewDto.ToModelOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tagDataReviewDto.Id, result.Id);
        Assert.Equal(tagDataReviewDto.CreatedDate, result.CreatedDate);
        Assert.Equal(tagDataReviewDto.ModifiedDate, result.ModifiedDate);
        Assert.Equal(tagDataReviewDto.TagNo, result.TagNo);
        Assert.Equal(tagDataReviewDto.Status, result.Status);
        Assert.Equal(tagDataReviewDto.ApproverId, result.ApproverId);
        Assert.Equal(tagDataReviewDto.CommentResponsible, result.CommentResponsible);
        Assert.Equal(tagDataReviewDto.Approved, result.Approved);
        Assert.Equal(tagDataReviewDto.TagDataVersion, result.TagDataVersion);
        Assert.NotNull(result.Conversations);
    }

    [Fact]
    public void ToModelOrNull_WithNullTagDataCommentDtos_ReturnsTagDataReviewWithEmptyComments()
    {
        // Arrange
        var tagDataReviewDto = GetTagDataReviewDto("Tag-006");

        // Act
        var result = tagDataReviewDto.ToModelOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tagDataReviewDto.Id, result.Id);
        Assert.Equal(tagDataReviewDto.CreatedDate, result.CreatedDate);
        Assert.Equal(tagDataReviewDto.ModifiedDate, result.ModifiedDate);
        Assert.Equal(tagDataReviewDto.TagNo, result.TagNo);
        Assert.Equal(tagDataReviewDto.Status, result.Status);
        Assert.Equal(tagDataReviewDto.ApproverId, result.ApproverId);
        Assert.Equal(tagDataReviewDto.CommentResponsible, result.CommentResponsible);
        Assert.Equal(tagDataReviewDto.Approved, result.Approved);
        Assert.Equal(tagDataReviewDto.TagDataVersion, result.TagDataVersion);
        Assert.NotNull(result.Conversations);
        Assert.Empty(result.Conversations);
    }

    [Fact]
    public void ToModelOrNull_WithNonNullTagDataCommentDtos_ReturnsTagDataReviewWithComments()
    {
        // Arrange
        var tagDataReviewDto = GetTagDataReviewDto("Tag-007");
        // Act
        var result = tagDataReviewDto.ToModelOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tagDataReviewDto.Id, result.Id);
        Assert.Equal(tagDataReviewDto.CreatedDate, result.CreatedDate);
        Assert.Equal(tagDataReviewDto.ModifiedDate, result.ModifiedDate);
        Assert.Equal(tagDataReviewDto.TagNo, result.TagNo);
        Assert.Equal(tagDataReviewDto.Status, result.Status);
        Assert.Equal(tagDataReviewDto.ApproverId, result.ApproverId);
        Assert.Equal(tagDataReviewDto.CommentResponsible, result.CommentResponsible);
        Assert.Equal(tagDataReviewDto.Approved, result.Approved);
        Assert.Equal(tagDataReviewDto.TagDataVersion, result.TagDataVersion);
        Assert.NotNull(result.Conversations);

    }

    private static Conversation GetConversation()
    {
        return new Conversation
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
        };
    }

    private static TagDataReview GetTagDataReview(string tagNo, int version)
    {
        return new TagDataReview
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            TagNo = tagNo,
            Status = ReviewStatusEnum.New,
            ApproverId = Guid.NewGuid(),
            CommentResponsible = Guid.NewGuid(),
            Approved = true,
            TagDataVersion = version,
            Conversations = new List<Conversation>
                    { GetConversation(), GetConversation()},
        };
    }

    private static TagDataReviewDto GetTagDataReviewDto(string tagNo)
    {
        return new TagDataReviewDto
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            TagNo = tagNo,
            Status = ReviewStatusEnum.New,
            ApproverId = Guid.NewGuid(),
            CommentResponsible = Guid.NewGuid(),
            Approved = true,
            TagDataVersion = 1,
        };
    }
}
