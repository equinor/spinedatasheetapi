namespace datasheetapi.Models;

public class Review : BaseEntity
{
    public ReviewStatusEnum Status { get; set; }
    public int TagId { get; set; }
    public int RevisionId { get; set; }
    public int UserId { get; set; }
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