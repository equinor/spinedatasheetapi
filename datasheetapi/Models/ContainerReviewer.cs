namespace datasheetapi.Models;

public class ContainerReviewer
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public ContainerReviewerStateEnum State { get; set; }

    public Guid ContainerReviewId { get; set; }
    public ContainerReview ContainerReview { get; set; } = null!;
    public ICollection<TagReviewer> TagReviewers { get; set; } = new List<TagReviewer>();
}

public enum ContainerReviewerStateEnum
{
    Open,
    Complete,
    Abandoned
}
