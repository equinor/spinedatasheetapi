namespace datasheetapi.Models;

public class ContainerReviewer
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public ContainerReviewerStateEnum State { get; set; }

    public ContainerReview ContainerReview { get; set; } = null!;
    public Guid ContainerReviewId { get; set; }
}

public enum ContainerReviewerStateEnum
{
    Complete,
    Abandoned
}
