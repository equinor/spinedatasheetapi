namespace datasheetapi.Models;

public class RevisionContainerReview : BaseEntity
{
    public Guid TagId { get; set; }
    public Guid RevisionId { get; set; }
    public ReviewStatusEnum Status { get; set; }
    public Guid ApproverId { get; set; }
    public Guid CommentResponsible { get; set; }
    public bool Approved { get; set; }
    public bool TagDataVersion { get; init; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}