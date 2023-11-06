using System.ComponentModel.DataAnnotations;

using datasheetapi.Dtos.ContainerReviewer;

namespace datasheetapi.Dtos.TagReviewer;
public record UpdateContainerReviewerDto
{
    [Required]
    public ContainerReviewerStateEnumDto State { get; set; }
}
