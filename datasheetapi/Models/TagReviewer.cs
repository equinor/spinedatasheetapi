namespace datasheetapi.Models;

public class TagReviewer
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string TagNo { get; set; } = string.Empty;
    public TagReviewerStateEnum State { get; set; }

    public Guid ContainerReviewerId { get; set; }
    public ContainerReviewer ContainerReviewer { get; set; } = null!;
}

public enum TagReviewerStateEnum
{
    NotReviewed,
    Reviewed
}
