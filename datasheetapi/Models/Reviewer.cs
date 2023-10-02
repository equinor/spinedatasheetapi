namespace datasheetapi.Models;

public class Reviewer : BaseEntity
{
    public Guid ReviewerId { get; set; }
    public Project Project { get; set; } = null!;
    public List<ReviewerTagDataReview> ReviewerReviews { get; set; } = new List<ReviewerTagDataReview>();
}
