namespace datasheetapi.Models;

public class ElectricalTagData : TagData
{
    public ElectricalPurchaserRequirement? ElectricalPurchaserRequirement { get; set; }
    public ElectricalSupplierOfferedProduct? ElectricalSupplierOfferedProduct { get; set; }
}
