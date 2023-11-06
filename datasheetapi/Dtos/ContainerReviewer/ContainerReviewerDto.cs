using datasheetapi.Dtos.TagReviewer;

namespace datasheetapi.Dtos.ContainerReviewer;

public class ContainerReviewerDto
{
    public Guid Id { get; set; }
    public ContainerReviewerStateEnumDto State { get; set; }
    public Guid UserId { get; set; }

    public Guid ContainerReviewId { get; set; }
    public List<TagReviewerDto> TagReviewers { get; set; } = new List<TagReviewerDto>();
}
