using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos.TagReviewer;
public record UpdateTagReviewerDto
{
    [Required]
    public TagReviewerStateEnumDto State { get; set; }
}
