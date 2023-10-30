using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos.TagReviewer;
public record UpdateReviewerDto
{
    [Required]
    public ReviewStatusDto ReviewStatus { get; set; }
}
