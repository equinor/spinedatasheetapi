namespace datasheetapi.Dtos;

public class MechanicalTagDataDto : TagDataDto
{
    public MechanicalPurchaserRequirement? MechanicalPurchaserRequirement { get; set; }
    public MechanicalSupplierOfferedProduct? MechanicalSupplierOfferedProduct { get; set; }
}
