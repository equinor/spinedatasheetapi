using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Models;

public class Conversation : BaseEntity
{
    public string? Property { get; set; }
    public ConversationLevel ConversationLevel { get; set; }
    public ConversationStatus ConversationStatus { get; set; }
    public List<Participant> Participants { get; set; } = new List<Participant>();
    public List<Message> Messages { get; set; } = new List<Message>();
    // TODO: This should be not null ?
    public Guid? TagDataReviewId { get; set; }
    public TagDataReview? TagDataReview { get; set; }

    public bool IsTagDataReviewComment => TagDataReviewId.HasValue;
    public bool IsEdited { get; set; }

    public void SetTagDataReview(TagDataReview review)
    {
        TagDataReviewId = review.Id;
        TagDataReview = review;
    }
}

public enum ConversationLevel
{
    Tag,
    PurchaserRequirement,
    SupplierOfferedValue,
}


public enum ConversationStatus
{
    Open,
    To_be_implemented,
    Closed,
    Implemented
}
