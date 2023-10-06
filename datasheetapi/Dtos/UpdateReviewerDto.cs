using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record UpdateReviewerDto
{
    [Required]
    public ReviewStatusDto ReviewStatus { get; set; }
}
