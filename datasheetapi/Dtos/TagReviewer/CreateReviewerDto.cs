using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos.TagReviewer;
public record CreateReviewerDto
{
    [Required]
    [NotEmptyGuid]
    public Guid ReviewerId { get; set; }
}
