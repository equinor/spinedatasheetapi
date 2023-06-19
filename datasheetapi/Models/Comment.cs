namespace datasheetapi.Models;

public class Comment : BaseEntity
{
    public Guid UserId { get; set; }
    public string CommenterName { get; set; } = string.Empty;
    public Guid TagDataId { get; set; }
    public Guid ReviewId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? Property { get; set; }
    public CommentLevel CommentLevel { get; set; }
    public bool External { get; set; }
}

public enum CommentLevel
{
    Tag,
    PurchaserRequirement,
    SupplierOfferedValue,
}
