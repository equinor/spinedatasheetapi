namespace datasheetapi.Adapters;
public static class CommentAdapter
{
    public static CommentDto? ToDtoOrNull(this Conversation? comment)
    {
        if (comment is null) { return null; }
        return comment.ToDto();
    }

    public static CommentDto? ToDtoOrNull(this Message? comment, string commenterName)
    {
        if (comment is null) { return null; }
        return comment.ToDto(commenterName);
    }

    private static CommentDto ToDto(this Message comment, string commenterName)
    {
        return new CommentDto
        {
            Id = comment.Id,
            UserId = comment.UserId,
            CommenterName = commenterName,
            Text = comment.SoftDeleted ? "" : comment.Text,
            // TODO: Does it require all these values?
            //Property = comment.Property,
            //CommentLevel = comment.ConversationLevel,
            //TagDataReviewId = comment.TagDataReviewId,
            //RevisionContainerReviewId = comment.RevisionContainerReviewId,
            CreatedDate = comment.CreatedDate,
            ModifiedDate = comment.ModifiedDate
        };
    }

    private static CommentDto ToDto(this Conversation comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            //TODO: fix this create part
            //UserId = comment.UserId,
            //CommenterName = comment.CommenterName,
            //Text = comment.Text,
            Property = comment.Property,
            CommentLevel = comment.ConversationLevel,
            TagDataReviewId = comment.TagDataReviewId,
            CreatedDate = comment.CreatedDate,
            ModifiedDate = comment.ModifiedDate,
            IsEdited = comment.IsEdited,
            SoftDeleted = comment.SoftDeleted,
        };
    }

    public static List<CommentDto> ToDto(this List<Conversation>? comments)
    {
        if (comments is null) { return new List<CommentDto>(); }
        return comments.Select(ToDto).ToList();
    }

    public static List<CommentDto> ToDto(this List<Message>? comments)
    {
        if (comments is null) { return new List<CommentDto>(); }
        //TODO: Add the username
        return comments.Select(comment => comment.ToDto("df")).ToList();
    }

    public static Conversation? ToModelOrNull(this CommentDto? commentDto)
    {
        if (commentDto is null) { return null; }
        return commentDto.ToModel();
    }

    public static Message? ToMessageModelOrNull(this CommentDto? commentDto)
    {
        if (commentDto is null) { return null; }
        return commentDto.ToMessageModel();
    }

    private static Conversation ToModel(this CommentDto commentDto)
    {
        return new Conversation
        {
            //TODO: why are are asking frontend to set the commentId, shouldnt be auto generated ?
            Id = commentDto.Id,
            Property = commentDto.Property,
            ConversationLevel = commentDto.CommentLevel,
            TagDataReviewId = commentDto.TagDataReviewId,
           // RevisionContainerReviewId = commentDto.RevisionContainerReviewId,
            CreatedDate = commentDto.CreatedDate,
            ModifiedDate = commentDto.ModifiedDate,
            Messages = new List<Message> { ToMessageModel(commentDto) },
            Participants = new List<Participant> { ToParticipantModel(commentDto) }
        };
    }

    private static Message ToMessageModel(this CommentDto commentDto)
    {
        return new Message
        {
            UserId = commentDto.UserId,
            Text = commentDto.Text,
            CreatedDate = commentDto.CreatedDate,
            ModifiedDate = commentDto.ModifiedDate,
            IsEdited = commentDto.IsEdited,
            SoftDeleted = commentDto.SoftDeleted,
        };
    }

    private static Participant ToParticipantModel(this CommentDto commentDto)
    {
        return new Participant
        {
            UserId = commentDto.UserId,
            CreatedDate = commentDto.CreatedDate,
            ModifiedDate = commentDto.ModifiedDate
        };
    }

    public static List<Conversation> ToModel(this List<CommentDto>? commentDtos)
    {
        if (commentDtos is null) { return new List<Conversation>(); }
        return commentDtos.Select(ToModel).ToList();
    }
}
