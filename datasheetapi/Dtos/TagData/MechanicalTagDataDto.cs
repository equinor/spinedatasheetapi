namespace datasheetapi.Dtos;

public record MechanicalTagDataDto : TagDataDto
{
    public MechanicalPurchaserRequirement? MechanicalPurchaserRequirement { get; set; }
    public MechanicalSupplierOfferedProduct? MechanicalSupplierOfferedProduct { get; set; }
}
