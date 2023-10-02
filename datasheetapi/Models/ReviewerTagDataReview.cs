namespace datasheetapi.Models;

public class ReviewerTagDataReview
{
    public ReviewStatusEnum Status { get; set; }

    // Relationships
    public Guid TagDataReviewId { get; set; }
    public Guid ReviewerId { get; set; }
}

