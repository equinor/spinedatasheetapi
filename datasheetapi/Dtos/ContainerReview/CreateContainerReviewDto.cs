using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos.ContainerReview;
public record CreateContainerReviewDto
{
    [NotEmptyGuid]
    public Guid RevisionContainerId { get; set; }
    [Required]
    public ContainerReviewStateEnumDto State { get; set; }
}
