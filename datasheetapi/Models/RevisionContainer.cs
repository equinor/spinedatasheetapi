namespace datasheetapi.Models;

public class RevisionContainer : BaseEntity
{
    public string RevisionContainerName { get; set; } = string.Empty;
    public int RevisionNumber { get; set; }
    public DateTimeOffset RevisionContainerDate { get; set; } = DateTimeOffset.Now;

    // Relationships
    public List<ITagData> TagData { get; set; } = new List<ITagData>();
    public RevisionContainerReview? RevisionContainerReview { get; set; }
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }
}
