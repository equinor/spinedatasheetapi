namespace datasheetapi.Models;

public class Reviewer
{
    public ReviewStatusEnum Status { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

    public Guid TagDataReviewId { get; set; }
    public Guid ReviewerId { get; set; }
}
