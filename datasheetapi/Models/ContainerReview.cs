namespace datasheetapi.Models;

public class ContainerReview
{
    public Guid Id { get; set; }

    public ContainerReviewStateEnum State { get; set; }
    public Guid CommentResponsible { get; set; }


    public Guid ContainerId { get; set; }
    public ICollection<ContainerReviewer> ContainerReviewers { get; set; } = new List<ContainerReviewer>();
}

public enum ContainerReviewStateEnum
{
    Active,
    SentToContractor
}
