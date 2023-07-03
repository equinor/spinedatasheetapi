using System.Text.Json.Serialization;

namespace datasheetapi.Models;

public class Contract : BaseEntity
{
    [JsonIgnore]
    public List<RevisionContainer> RevisionContainers { get; set; } = new List<RevisionContainer>();
    public string ContractName { get; set; } = string.Empty;
}