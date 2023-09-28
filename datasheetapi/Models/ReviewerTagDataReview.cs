using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Models;

public class ReviewerTagDataReview : BaseEntity
{
    public ReviewStatusEnum Status { get; set; }
    public Guid ReviewerId { get; set; }

    // Relationships
    public TagDataReview TagDataReview { get; set; } = null!;
}

