namespace datasheetapi.Models;

public class Container
{
    public Guid Id { get; set; }
    public string ContainerName { get; set; } = string.Empty;
    public int RevisionNumber { get; set; }
    public DateTimeOffset ContainerDate { get; set; } = DateTimeOffset.Now;
    public List<ContainerTags> Tags { get; set; } = new List<ContainerTags>();

    public Guid ContractId { get; set; }
}
