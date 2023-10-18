namespace datasheetapi.Models;

public class TagReviewer
{
    public Guid Id { get; set; }

    public string Tag { get; set; } = null!;
    public Guid ProjectId { get; set; }
    public Guid ReviewerId { get; set; }

    public TagReviewerStateEnum State { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

}

public enum TagReviewerStateEnum
{
    NotReviewed,
    Reviewed
}
