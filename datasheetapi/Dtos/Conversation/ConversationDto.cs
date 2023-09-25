using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record ConversationDto
{
    [MaxLength(500)]
    [Required]
    public string Text { get; set; } = string.Empty;
    public string? Property { get; set; }
    public ConversationLevel ConversationLevel { get; set; }
    public ConversationStatus ConversationStatus { get; set; }
}
