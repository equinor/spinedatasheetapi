namespace datasheetapi.Models;

public class Contract
{
    public Guid Id { get; set; }
    public string ContractName { get; set; } = string.Empty;

    // Relationships
    public Guid ProjectId { get; set; }
    public List<Container> Containers { get; set; } = new List<Container>();
}
