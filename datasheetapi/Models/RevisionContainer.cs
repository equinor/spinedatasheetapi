using System.Text.Json.Serialization;

namespace datasheetapi.Models;

public class RevisionContainer : BaseEntity
{
    public RevisionContainer(Contract contract)
    {
        Contract = contract;
        AddRevisionContainerToContract(contract);
    }

    private void AddRevisionContainerToContract(Contract contract)
    {
        contract.RevisionContainers.Add(this);
    }

    [JsonIgnore]
    public List<TagData> TagData { get; set; } = new List<TagData>();
    public RevisionContainerReview? RevisionContainerReview { get; set; }
    public Contract Contract { get; set; }
    public string RevisionContainerName { get; set; } = string.Empty;
    public DateTimeOffset RevisionContainerDate { get; set; } = DateTimeOffset.Now;
    public int RevisionNumber { get; set; }
}
