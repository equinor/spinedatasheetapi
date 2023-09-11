using datasheetapi.Adapters;
using datasheetapi.Dtos;
using datasheetapi.Models;

namespace tests.Adapters;

public class CommentAdapterTests
{
    [Fact]
    public void ToConversationModel()
    {
        CreateCommentDto comment = new()
        {
            Property = "TagNumber",
            Text = "Text to add",
            ConversationLevel = ConversationLevel.Tag,
            ConversationStatus = ConversationStatus.Open,
        };
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var result = comment.ToModel(reviewId, userId);

        Assert.NotNull(result);
        Assert.Single(result.Participants);
        Assert.Single(result.Messages);
        Assert.Equal(result.TagDataReviewId, reviewId);
        Assert.Equal(result.Property, comment.Property);
        Assert.Equal(result.Messages[0].Text, comment.Text);
        Assert.Equal(result.Messages[0].UserId, userId);
        Assert.Equal(result.Participants[0].UserId, userId);
    }

    [Fact]
    public void ToConversationDto()
    {
        var userId = Guid.NewGuid();
        var conversationId = Guid.NewGuid();
        var userName = "Some username";
        var property = "Some property";
        var message = "message";
        Conversation conversation = 
                GetConversation(userId, conversationId, property, message);

        Dictionary<Guid, string> userIdName = new()
        {
            { userId, userName }
        };

        var result = conversation.ToDto(userIdName);

        Assert.NotNull(result);
        Assert.Single(result.Participants);
        Assert.NotNull(result.Messages);
        Assert.Single(result.Messages);
        Assert.Equal(result.Id, conversationId);
        Assert.Equal(result.Property, property);
        Assert.Equal(result.Messages[0].Text, message);
        Assert.Equal(result.Messages[0].UserId, userId);
        Assert.Equal(result.Participants[0].UserId, userId);
        Assert.Equal(result.Participants[0].UserName, userName);
    }

    [Fact]
    public void ToMessageDto_emptyMessage_whenSoftDeleted()
    {
        var userId = Guid.NewGuid();
        var conversationId = Guid.NewGuid();
        var message = GetMessage(userId, conversationId, 
                "message", true);
        var commenterName = "someName";
        var result = message.ToMessageDto(commenterName);

        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.True(result.SoftDeleted);
        Assert.Equal("", result.Text);
        Assert.Equal(commenterName, result.CommenterName);
    }

    [Fact]
    public void ToMessageModel()
    {
        var userId = Guid.NewGuid();
        var inputText = "somemessage";
        var messageDto = new MessageDto()
        {
            Text = inputText,
        };
        var result = messageDto.ToMessageModel(userId);

        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(inputText, result.Text);
    }
    

    private static Conversation GetConversation(Guid userId,
         Guid conversationId, string property, string message)
    {
        return new()
        {
            Property = property,
            ConversationLevel = ConversationLevel.Tag,
            ConversationStatus = ConversationStatus.Open,
            Id = conversationId,
            Participants = new List<Participant> { GetParticipant(userId, conversationId)
            },
            Messages = new List<Message> { GetMessage(userId, conversationId, message, false)
            },
        };
    }

    private static Participant GetParticipant(Guid userId,
             Guid conversationId)
    {
        return new()
        {
            UserId = userId,
            ConversationId = conversationId,
        };
    }

    private static Message GetMessage(Guid userId,
         Guid conversationId, string message, bool softDeleted)
    {
        return new()
        {
            UserId = userId,
            Text = message,
            ConversationId = conversationId,
            IsEdited = true,
            SoftDeleted = softDeleted,
        };
    }
}