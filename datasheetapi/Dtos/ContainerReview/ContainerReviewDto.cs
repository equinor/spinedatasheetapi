using datasheetapi.Dtos.ContainerReviewer;

namespace datasheetapi.Dtos.ContainerReview;
public record ContainerReviewDto
{
    public Guid Id { get; set; }
    public ContainerReviewStateEnumDto State { get; set; }
    public Guid CommentResponsible { get; set; }

    public Guid ContainerId { get; set; }
    public List<ContainerReviewerDto> ContainerReviewers { get; set; } = new List<ContainerReviewerDto>();
}
