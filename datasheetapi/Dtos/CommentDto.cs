using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record CommentDto : BaseEntityDto
{
    public Guid UserId { get; set; }
    public string CommenterName { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Text { get; set; } = string.Empty;
    public string? Property { get; set; }
    public CommentLevel CommentLevel { get; set; }
    public Guid? TagDataReviewId { get; set; }
    public Guid? RevisionContainerReviewId { get; set; }
    public bool IsEdited { get; set; }
    public bool SoftDeleted { get; set; }
}
