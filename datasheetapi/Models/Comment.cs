using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Models;

public class Comment : BaseEntity
{
    public Guid UserId { get; set; }
    public string CommenterName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Text { get; set; } = string.Empty;
    public string? Property { get; set; }
    public CommentLevel CommentLevel { get; set; }

    public Guid? TagDataReviewId { get; set; }
    public Guid? RevisionContainerReviewId { get; set; }
    public TagDataReview? TagDataReview { get; set; }
    public RevisionContainerReview? RevisionContainerReview { get; set; }

    public bool IsTagDataReviewComment => TagDataReviewId.HasValue;
    public bool IsRevisionContainerReviewComment => RevisionContainerReviewId.HasValue;
    public bool IsEdited { get; set; }
    public bool SoftDeleted { get; set; }

    public void SetTagDataReview(TagDataReview review)
    {
        TagDataReviewId = review.Id;
        TagDataReview = review;
        RevisionContainerReviewId = null;
        RevisionContainerReview = null;
    }

    public void SetRevisionContainerReview(RevisionContainerReview review)
    {
        RevisionContainerReviewId = review.Id;
        RevisionContainerReview = review;
        TagDataReviewId = null;
        TagDataReview = null;
    }
}

public enum CommentLevel
{
    Tag,
    PurchaserRequirement,
    SupplierOfferedValue,
}
