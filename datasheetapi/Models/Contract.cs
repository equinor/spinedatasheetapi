using System.Text.Json.Serialization;

namespace datasheetapi.Models;

public class Contract : BaseEntity
{
    [JsonIgnore]
    public List<RevisionPackage> Packages { get; set; } = new List<RevisionPackage>();
    public string ContractName { get; set; } = string.Empty;
}