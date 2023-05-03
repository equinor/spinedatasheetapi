namespace datasheetapi.Models;

public class Comment : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TagDataId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? Property { get; set; }
    public CommentLevel CommentLevel { get; set; }
}

public enum CommentLevel
{
    Tag,
    PurchaserRequirement,
    SupplierOfferedValue,
}