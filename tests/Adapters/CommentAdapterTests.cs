using datasheetapi.Adapters;
using datasheetapi.Dtos;
using datasheetapi.Models;

namespace tests.Adapters;


public class CommentAdapterTests
{
    [Fact]
    public void ToDtoOrNull_ReturnsNull_WhenCommentIsNull()
    {
        // Arrange
        Conversation? comment = null;

        // Act
        var result = comment.ToDtoOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToDtoOrNull_ReturnsCommentDto_WhenCommentIsNotNull()
    {
        // Arrange
        var comment = new Conversation
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CommenterName = "John Doe",
            Text = "This is a comment",
            Property = "Some property",
            ConversationLevel = ConversationLevel.PurchaserRequirement,
            TagDataReviewId = Guid.NewGuid(),
            RevisionContainerReviewId = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        // Act
        var result = comment.ToDtoOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comment.Id, result.Id);
        Assert.Equal(comment.UserId, result.UserId);
        Assert.Equal(comment.CommenterName, result.CommenterName);
        Assert.Equal(comment.Text, result.Text);
        Assert.Equal(comment.Property, result.Property);
        Assert.Equal(comment.ConversationLevel, result.CommentLevel);
        Assert.Equal(comment.TagDataReviewId, result.TagDataReviewId);
        Assert.Equal(comment.RevisionContainerReviewId, result.RevisionContainerReviewId);
        Assert.Equal(comment.CreatedDate, result.CreatedDate);
        Assert.Equal(comment.ModifiedDate, result.ModifiedDate);
    }

    [Fact]
    public void ToDto_ReturnsEmptyList_WhenCommentsIsNull()
    {
        // Arrange
        List<Conversation>? comments = null;

        // Act
        var result = comments.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToDto_ReturnsCommentDtos_WhenCommentsIsNotNull()
    {
        // Arrange
        var comments = new List<Conversation>
        {
            new Conversation
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CommenterName = "John Doe",
                Text = "This is a comment",
                Property = "Some property",
                ConversationLevel = ConversationLevel.PurchaserRequirement,
                TagDataReviewId = Guid.NewGuid(),
                RevisionContainerReviewId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            },
            new Conversation
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CommenterName = "Jane Smith",
                Text = "This is another comment",
                Property = "Some other property",
                ConversationLevel = ConversationLevel.PurchaserRequirement,
                TagDataReviewId = Guid.NewGuid(),
                RevisionContainerReviewId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            }
        };

        // Act
        var result = comments.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comments.Count, result.Count);
        for (int i = 0; i < comments.Count; i++)
        {
            Assert.Equal(comments[i].Id, result[i].Id);
            Assert.Equal(comments[i].UserId, result[i].UserId);
            Assert.Equal(comments[i].CommenterName, result[i].CommenterName);
            Assert.Equal(comments[i].Text, result[i].Text);
            Assert.Equal(comments[i].Property, result[i].Property);
            Assert.Equal(comments[i].ConversationLevel, result[i].CommentLevel);
            Assert.Equal(comments[i].TagDataReviewId, result[i].TagDataReviewId);
            Assert.Equal(comments[i].RevisionContainerReviewId, result[i].RevisionContainerReviewId);
            Assert.Equal(comments[i].CreatedDate, result[i].CreatedDate);
            Assert.Equal(comments[i].ModifiedDate, result[i].ModifiedDate);
        }
    }

    [Fact]
    public void ToModelOrNull_ReturnsNull_WhenCommentDtoIsNull()
    {
        // Arrange
        CommentDto? commentDto = null;

        // Act
        var result = commentDto.ToModelOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToModelOrNull_ReturnsComment_WhenCommentDtoIsNotNull()
    {
        // Arrange
        var commentDto = new CommentDto
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CommenterName = "John Doe",
            Text = "This is a comment",
            Property = "Some property",
            CommentLevel = ConversationLevel.PurchaserRequirement,
            TagDataReviewId = Guid.NewGuid(),
            RevisionContainerReviewId = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        // Act
        var result = commentDto.ToModelOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(commentDto.Id, result.Id);
        Assert.Equal(commentDto.UserId, result.UserId);
        Assert.Equal(commentDto.CommenterName, result.CommenterName);
        Assert.Equal(commentDto.Text, result.Text);
        Assert.Equal(commentDto.Property, result.Property);
        Assert.Equal(commentDto.CommentLevel, result.ConversationLevel);
        Assert.Equal(commentDto.TagDataReviewId, result.TagDataReviewId);
        Assert.Equal(commentDto.RevisionContainerReviewId, result.RevisionContainerReviewId);
        Assert.Equal(commentDto.CreatedDate, result.CreatedDate);
        Assert.Equal(commentDto.ModifiedDate, result.ModifiedDate);
    }

    [Fact]
    public void ToModel_ReturnsEmptyList_WhenCommentDtosIsNull()
    {
        // Arrange
        List<CommentDto>? commentDtos = null;

        // Act
        var result = commentDtos.ToModel();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToModel_ReturnsComments_WhenCommentDtosIsNotNull()
    {
        // Arrange
        var commentDtos = new List<CommentDto>
        {
            new CommentDto
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CommenterName = "John Doe",
                Text = "This is a comment",
                Property = "Some property",
                CommentLevel = ConversationLevel.PurchaserRequirement,
                TagDataReviewId = Guid.NewGuid(),
                RevisionContainerReviewId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            },
            new CommentDto
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CommenterName = "Jane Smith",
                Text = "This is another comment",
                Property = "Some other property",
                CommentLevel = ConversationLevel.PurchaserRequirement,
                TagDataReviewId = Guid.NewGuid(),
                RevisionContainerReviewId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            }
        };

        // Act
        var result = commentDtos.ToModel();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(commentDtos.Count, result.Count);
        for (int i = 0; i < commentDtos.Count; i++)
        {
            Assert.Equal(commentDtos[i].Id, result[i].Id);
            Assert.Equal(commentDtos[i].UserId, result[i].UserId);
            Assert.Equal(commentDtos[i].CommenterName, result[i].CommenterName);
            Assert.Equal(commentDtos[i].Text, result[i].Text);
            Assert.Equal(commentDtos[i].Property, result[i].Property);
            Assert.Equal(commentDtos[i].CommentLevel, result[i].ConversationLevel);
            Assert.Equal(commentDtos[i].TagDataReviewId, result[i].TagDataReviewId);
            Assert.Equal(commentDtos[i].RevisionContainerReviewId, result[i].RevisionContainerReviewId);
            Assert.Equal(commentDtos[i].CreatedDate, result[i].CreatedDate);
            Assert.Equal(commentDtos[i].ModifiedDate, result[i].ModifiedDate);
        }
    }
}
