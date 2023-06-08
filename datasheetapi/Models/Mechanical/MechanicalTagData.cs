namespace datasheetapi.Models.Mechanical;

public class MechanicalTagData: TagData
{
    public MechanicalPurchaserRequirement? MechanicalPurchaserRequirement { get; set; }
    public MechanicalSupplierOfferedProduct? MechanicalSupplierOfferedProduct { get; set; }
}