namespace datasheetapi.Models;

public class ElectricalTagData : TagData
{
    public ElectricalTagData() : base()
    {
    }

    public ElectricalPurchaserRequirement? ElectricalPurchaserRequirement { get; set; }
    public ElectricalSupplierOfferedProduct? ElectricalSupplierOfferedProduct { get; set; }
}
