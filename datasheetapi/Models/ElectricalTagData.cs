namespace datasheetapi.Models;

public class ElectricalTagData : TagData
{
    public ElectricalTagData(Package package) : base(package)
    {
    }

    public ElectricalPurchaserRequirement? ElectricalPurchaserRequirement { get; set; }
    public ElectricalSupplierOfferedProduct? ElectricalSupplierOfferedProduct { get; set; }
}
