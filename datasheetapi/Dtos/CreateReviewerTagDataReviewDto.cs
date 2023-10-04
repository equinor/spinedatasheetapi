using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record CreateReviewerTagDataReviewDto
{
    [Required]
    [NotEmptyGuid]
    public Guid ReviewerId { get; set; }
}
