using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record CreateCommentDto
{
    [MaxLength(500)]
    public string Text { get; set; } = string.Empty;
    public string? Property { get; set; }
    public ConversationLevel CommentLevel { get; set; }
    public ConversationStatus ConversationStatus { get; set; }
}
