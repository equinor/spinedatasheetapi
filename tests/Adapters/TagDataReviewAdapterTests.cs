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
        // Arrange
        var tagDataReview = new TagDataReview
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            TagNo = "Tag-002",
            Status = ReviewStatusEnum.New,
            ApproverId = Guid.NewGuid(),
            CommentResponsible = Guid.NewGuid(),
            Approved = true,
            TagDataVersion = 1,
            Comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Text = "Comment 1",
                    UserId = Guid.NewGuid(),
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Text = "Comment 2",
                    UserId = Guid.NewGuid(),
                },
            },
        };

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
        Assert.NotNull(result.Comments);
        Assert.Equal(tagDataReview.Comments.Count, result.Comments.Count);
        for (int i = 0; i < tagDataReview.Comments.Count; i++)
        {
            Assert.Equal(tagDataReview.Comments[i].Id, result.Comments[i].Id);
            Assert.Equal(tagDataReview.Comments[i].CreatedDate, result.Comments[i].CreatedDate);
            Assert.Equal(tagDataReview.Comments[i].ModifiedDate, result.Comments[i].ModifiedDate);
            Assert.Equal(tagDataReview.Comments[i].Text, result.Comments[i].Text);
            Assert.Equal(tagDataReview.Comments[i].UserId, result.Comments[i].UserId);
        }
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
        {
            new TagDataReview
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                TagNo = "Tag-003",
                Status = ReviewStatusEnum.New,
                ApproverId = Guid.NewGuid(),
                CommentResponsible = Guid.NewGuid(),
                Approved = true,
                TagDataVersion = 1,
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        Text = "Comment 1",
                        UserId = Guid.NewGuid(),
                    },
                    new Comment
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        Text = "Comment 2",
                        UserId = Guid.NewGuid(),
                    },
                },
            },
            new TagDataReview
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                TagNo = "Tag-004",
                Status = ReviewStatusEnum.New,
                ApproverId = Guid.NewGuid(),
                CommentResponsible = Guid.NewGuid(),
                Approved = false,
                TagDataVersion = 2,
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        Text = "Comment 3",
                        UserId = Guid.NewGuid(),
                    },
                },
            },
        };

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
            Assert.NotNull(result[i].Comments);
            Assert.Equal(tagDataReviews[i].Comments.Count, result[i].Comments?.Count);
            for (int j = 0; j < tagDataReviews[i].Comments.Count; j++)
            {
                Assert.Equal(tagDataReviews[i].Comments[j].Id, result[i].Comments?[j].Id);
                Assert.Equal(tagDataReviews[i].Comments[j].CreatedDate, result[i].Comments?[j].CreatedDate);
                Assert.Equal(tagDataReviews[i].Comments[j].ModifiedDate, result[i].Comments?[j].ModifiedDate);
                Assert.Equal(tagDataReviews[i].Comments[j].Text, result[i].Comments?[j].Text);
                Assert.Equal(tagDataReviews[i].Comments[j].UserId, result[i].Comments?[j].UserId);
            }
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
        var tagDataReviewDto = new TagDataReviewDto
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            TagNo = "Tag-005",
            Status = ReviewStatusEnum.New,
            ApproverId = Guid.NewGuid(),
            CommentResponsible = Guid.NewGuid(),
            Approved = true,
            TagDataVersion = 1,
            Comments = new List<CommentDto>
            {
                new CommentDto
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Text = "Comment 1",
                    UserId = Guid.NewGuid(),
                },
                new CommentDto
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Text = "Comment 2",
                    UserId = Guid.NewGuid(),
                },
            },
        };

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
        Assert.NotNull(result.Comments);
        Assert.Equal(tagDataReviewDto.Comments.Count, result.Comments.Count);
        for (int i = 0; i < tagDataReviewDto.Comments.Count; i++)
        {
            Assert.Equal(tagDataReviewDto.Comments[i].Id, result.Comments[i].Id);
            Assert.Equal(tagDataReviewDto.Comments[i].CreatedDate, result.Comments[i].CreatedDate);
            Assert.Equal(tagDataReviewDto.Comments[i].ModifiedDate, result.Comments[i].ModifiedDate);
            Assert.Equal(tagDataReviewDto.Comments[i].Text, result.Comments[i].Text);
            Assert.Equal(tagDataReviewDto.Comments[i].UserId, result.Comments[i].UserId);
        }
    }

    [Fact]
    public void ToModelOrNull_WithNullTagDataCommentDtos_ReturnsTagDataReviewWithEmptyComments()
    {
        // Arrange
        var tagDataReviewDto = new TagDataReviewDto
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            TagNo = "Tag-006",
            Status = ReviewStatusEnum.New,
            ApproverId = Guid.NewGuid(),
            CommentResponsible = Guid.NewGuid(),
            Approved = true,
            TagDataVersion = 1,
            Comments = null,
        };

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
        Assert.NotNull(result.Comments);
        Assert.Empty(result.Comments);
    }

    [Fact]
    public void ToModelOrNull_WithNonNullTagDataCommentDtos_ReturnsTagDataReviewWithComments()
    {
        // Arrange
        var tagDataReviewDto = new TagDataReviewDto
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            TagNo = "Tag-007",
            Status = ReviewStatusEnum.New,
            ApproverId = Guid.NewGuid(),
            CommentResponsible = Guid.NewGuid(),
            Approved = true,
            TagDataVersion = 1,
            Comments = new List<CommentDto>
            {
                new CommentDto
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Text = "Comment 1",
                    UserId = Guid.NewGuid(),
                },
                new CommentDto
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Text = "Comment 2",
                    UserId = Guid.NewGuid(),
                },
            },
        };

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
        Assert.NotNull(result.Comments);
        Assert.Equal(tagDataReviewDto.Comments.Count, result.Comments.Count);
        for (int i = 0; i < tagDataReviewDto.Comments.Count; i++)
        {
            Assert.Equal(tagDataReviewDto.Comments[i].Id, result.Comments[i].Id);
            Assert.Equal(tagDataReviewDto.Comments[i].CreatedDate, result.Comments[i].CreatedDate);
            Assert.Equal(tagDataReviewDto.Comments[i].ModifiedDate, result.Comments[i].ModifiedDate);
            Assert.Equal(tagDataReviewDto.Comments[i].Text, result.Comments[i].Text);
            Assert.Equal(tagDataReviewDto.Comments[i].UserId, result.Comments[i].UserId);
        }
    }
}
