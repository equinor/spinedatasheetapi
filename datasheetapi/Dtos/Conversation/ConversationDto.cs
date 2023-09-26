using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record ConversationDto
{
    [MaxLength(500)]
    [Required]
    public string Text { get; set; } = string.Empty;
    public string? Property { get; set; }
    public ConversationLevelDto ConversationLevel { get; set; }
    public ConversationStatusDto ConversationStatus { get; set; }
}
