namespace datasheetapi.Models;

public class RevisionContainerReview : BaseEntity
{
    public ReviewStatusEnum Status { get; set; }
    public Guid ApproverId { get; set; }
    public Guid CommentResponsible { get; set; }
    public bool Approved { get; set; }
    public int RevisionContainerVersion { get; init; }
    public Guid RevisionContainerId { get; set; }
    public RevisionContainer? RevisionContainer { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
