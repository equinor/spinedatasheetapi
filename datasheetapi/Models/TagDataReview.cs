namespace datasheetapi.Models;

public class TagDataReview : BaseEntity
{
    public ReviewStatusEnum Status { get; set; }
    public Guid ApproverId { get; set; }
    public Guid CommentResponsible { get; set; }
    public bool Approved { get; set; }
    public int TagDataVersion { get; init; }

    // Relationships
    public string? TagNo { get; set; }
    public List<Conversation> Conversations { get; set; } = new List<Conversation>();
    public List<ReviewerTagDataReview> ReviewerReviews { get; set; } = new List<ReviewerTagDataReview>();
}

public enum ReviewStatusEnum
{
    New = 0,
    Reviewed = 3,
    Resubmit = 4,
    Diff = 5,
    Duplicate = 6,
    ReviewedWithComment = 7,
    NotReviewed = 8,
    Deleted = 9,
}
