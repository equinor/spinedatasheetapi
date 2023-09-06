namespace datasheetapi.Adapters;
public static class CommentAdapter
{
    public static CommentDto? ToDtoOrNull(this Comment? comment)
    {
        if (comment is null) { return null; }
        return comment.ToDto();
    }

    private static CommentDto ToDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            UserId = comment.UserId,
            CommenterName = comment.CommenterName,
            Text = comment.SoftDeleted ? "" : comment.Text,
            Property = comment.Property,
            CommentLevel = comment.CommentLevel,
            TagDataReviewId = comment.TagDataReviewId,
            RevisionContainerReviewId = comment.RevisionContainerReviewId,
            CreatedDate = comment.CreatedDate,
            ModifiedDate = comment.ModifiedDate,
            IsEdited = comment.IsEdited,
            SoftDeleted = comment.SoftDeleted,
        };
    }

    public static List<CommentDto> ToDto(this List<Comment>? comments)
    {
        if (comments is null) { return new List<CommentDto>(); }
        return comments.Select(ToDto).ToList();
    }

    public static Comment? ToModelOrNull(this CommentDto? commentDto)
    {
        if (commentDto is null) { return null; }
        return commentDto.ToModel();
    }

    private static Comment ToModel(this CommentDto commentDto)
    {
        return new Comment
        {
            Id = commentDto.Id,
            UserId = commentDto.UserId,
            CommenterName = commentDto.CommenterName,
            Text = commentDto.Text,
            Property = commentDto.Property,
            CommentLevel = commentDto.CommentLevel,
            TagDataReviewId = commentDto.TagDataReviewId,
            RevisionContainerReviewId = commentDto.RevisionContainerReviewId,
            CreatedDate = commentDto.CreatedDate,
            ModifiedDate = commentDto.ModifiedDate,
            IsEdited = commentDto.IsEdited,
            SoftDeleted = commentDto.SoftDeleted,
        };
    }

    public static List<Comment> ToModel(this List<CommentDto>? commentDtos)
    {
        if (commentDtos is null) { return new List<Comment>(); }
        return commentDtos.Select(ToModel).ToList();
    }
}
