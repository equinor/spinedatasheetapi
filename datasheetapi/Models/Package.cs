using System.Text.Json.Serialization;

namespace datasheetapi.Models;

public class Package : BaseEntity
{
    public Package(Contract contract)
    {
        Contract = contract;
        AddContractToPackage(contract);
    }

    private void AddContractToPackage(Contract contract)
    {
        contract.Packages.Add(this);
    }

    [JsonIgnore]
    public List<TagData>? Tags { get; set; }
    public Contract Contract { get; set; }
}
