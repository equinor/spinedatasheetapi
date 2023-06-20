namespace datasheetapi.Models;

public class Review : BaseEntity
{
    public ReviewStatusEnum Status { get; set; }
    public Guid TagId { get; set; }
    public Guid RevisionId { get; set; }
    public Guid UserId { get; set; }
    public bool Conflict { get; set; }
    public bool Approved { get; set; }
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