namespace datasheetapi.Dtos;

public class ElectricalTagDataDto : TagDataDto
{
    public ElectricalPurchaserRequirement? ElectricalPurchaserRequirement { get; set; }
    public ElectricalSupplierOfferedProduct? ElectricalSupplierOfferedProduct { get; set; }
}
