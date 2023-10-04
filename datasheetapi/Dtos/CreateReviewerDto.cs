using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record CreateReviewerDto
{
    [Required]
    [NotEmptyGuid]
    public Guid ReviewerId { get; set; }
}
