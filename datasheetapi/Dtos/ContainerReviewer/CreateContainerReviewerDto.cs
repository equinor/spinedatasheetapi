using datasheetapi.Dtos.TagReviewer;

namespace datasheetapi.Dtos.ContainerReviewer;

public class CreateContainerReviewerDto
{
    public Guid UserId { get; set; }
    public List<CreateTagReviewerDto>? TagReviewers { get; set; }
}
