using System.Text.Json.Serialization;

namespace datasheetapi.Models;

public class Contract : BaseEntity
{
    [JsonIgnore]
    public List<Package> Packages { get; set; } = new List<Package>();
    public string ContractName { get; set; } = string.Empty;
}