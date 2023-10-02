using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record ReviewerTagDataReviewDto
{
    [Required]
    public ReviewStatusDto Status { get; set; }
    public Guid ReviewerId { get; set; }
    public Guid TagDataReviewId { get; set; }
}
