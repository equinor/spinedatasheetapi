using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos.TagReviewer;
public record UpdateReviewerDto
{
    [Required]
    public TagReviewerStateEnumDto State { get; set; }
}
