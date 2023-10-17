using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos;
public record ReviewerDto
{
    [Required]
    public ReviewStatusDto Status { get; set; }
    public string DisplayName { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

    public Guid ReviewerId { get; set; }
    public Guid TagDataReviewId { get; set; }
}
