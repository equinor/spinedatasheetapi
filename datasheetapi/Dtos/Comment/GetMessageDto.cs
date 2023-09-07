using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record GetMessageDto : BaseEntityDto
{
    public Guid UserId { get; set; }
    [MaxLength(500)]
    public string Text { get; set; } = string.Empty;
    public string CommenterName { get; set; } = string.Empty;
    public bool IsEdited { get; set; }
}
