using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record CreateReviewerTagDataReviewDto
{
    [Required]
    public ReviewStatusDto Status { get; set; }
}
