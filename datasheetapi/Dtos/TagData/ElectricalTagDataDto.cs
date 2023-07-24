namespace datasheetapi.Dtos;

public record ElectricalTagDataDto : TagDataDto
{
    public ElectricalPurchaserRequirement? ElectricalPurchaserRequirement { get; set; }
    public ElectricalSupplierOfferedProduct? ElectricalSupplierOfferedProduct { get; set; }
}
