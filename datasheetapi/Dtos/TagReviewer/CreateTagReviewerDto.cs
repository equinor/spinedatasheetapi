using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos.TagReviewer;
public record CreateTagReviewerDto
{
    [Required]
    [NotEmptyGuid]
    public Guid ReviewerId { get; set; }
    public string TagNo { get; set; } = null!;
}
