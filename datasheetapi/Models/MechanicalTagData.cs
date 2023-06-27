namespace datasheetapi.Models;

public class MechanicalTagData : TagData
{
    public MechanicalTagData(Package package) : base(package)
    {
    }

    public MechanicalPurchaserRequirement? MechanicalPurchaserRequirement { get; set; }
    public MechanicalSupplierOfferedProduct? MechanicalSupplierOfferedProduct { get; set; }
}
