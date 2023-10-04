using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record CreateTagDataReviewDto
{
    [Required]
    public string TagNo { get; set; } = string.Empty;
    public List<CreateReviewerTagDataReviewDto>? ReviewerTagDataReviews { get; set; }
}
