using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record CreateTagDataReviewDto
{
    [Required]
    public string TagNo { get; set; } = string.Empty;
    [Required]
    public ReviewStatusDto Status { get; set; }
}
