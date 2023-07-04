namespace datasheetapi.Models;

public class RevisionContainer : BaseEntity
{
    public string RevisionContainerName { get; set; } = string.Empty;
    public int RevisionNumber { get; set; }
    public DateTimeOffset RevisionContainerDate { get; set; } = DateTimeOffset.Now;

    // Relationships
    public List<TagData> TagData { get; set; } = new List<TagData>();
    public RevisionContainerReview? RevisionContainerReview { get; set; }
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }
}
