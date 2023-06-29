namespace datasheetapi.Models;

public class MechanicalTagData : TagData
{
    public MechanicalTagData() : base()
    {
    }

    public MechanicalPurchaserRequirement? MechanicalPurchaserRequirement { get; set; }
    public MechanicalSupplierOfferedProduct? MechanicalSupplierOfferedProduct { get; set; }
}
