namespace datasheetapi.Dtos;
public record ContainerReviewDto : BaseEntityDto
{
    public ReviewStatusDto Status { get; set; }
    public Guid ApproverId { get; set; }
    public Guid CommentResponsible { get; set; }
    public bool Approved { get; set; }
    public int ContainerVersion { get; init; }
    public Guid ContainerId { get; set; }
    public ContainerDto? Container { get; set; }
}
