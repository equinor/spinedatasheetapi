using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos.ContainerReview;
public record CreateContainerReviewDto
{
    [NotEmptyGuid]
    public Guid RevisionContainerId { get; set; }
    [Required]
    public ReviewStatusDto Status { get; set; }
}
