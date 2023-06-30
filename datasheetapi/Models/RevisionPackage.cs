using System.Text.Json.Serialization;

namespace datasheetapi.Models;

public class RevisionPackage : BaseEntity
{
    public RevisionPackage(Contract contract)
    {
        Contract = contract;
        AddContractToPackage(contract);
    }

    private void AddContractToPackage(Contract contract)
    {
        contract.Packages.Add(this);
    }

    [JsonIgnore]
    public List<TagData> TagData { get; set; } = new List<TagData>();
    public Contract Contract { get; set; }
    public string PackageName { get; set; } = string.Empty;
    public DateTimeOffset PackageDate { get; set; } = DateTimeOffset.Now;
    public int RevisionNumber { get; set; }
}
