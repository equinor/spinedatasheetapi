namespace datasheetapi.Dtos;
public record RevisionContainerReviewDto : BaseEntityDto
{
    public ReviewStatusDto Status { get; set; }
    public Guid ApproverId { get; set; }
    public Guid CommentResponsible { get; set; }
    public bool Approved { get; set; }
    public int RevisionContainerVersion { get; init; }
    public Guid RevisionContainerId { get; set; }
    public RevisionContainerDto? RevisionContainer { get; set; }
}
