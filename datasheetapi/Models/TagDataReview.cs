using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Models;

public class TagDataReview : BaseEntity
{
    public ReviewStatusEnum Status { get; set; }
    public Guid ApproverId { get; set; }
    public Guid CommentResponsible { get; set; }
    public bool Approved { get; set; }
    public int TagDataVersion { get; init; }

    // Relationships
    public Guid TagDataId { get; set; }
    public TagData? TagData { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
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