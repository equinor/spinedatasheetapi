namespace datasheetapi.Dtos;

public class DataSheetDto
{
    public Guid Id { get; set; }
    public PurchaserRequirement PurchaserRequirement { get; set; }
    public SupplierOfferedProduct SupplierOfferedProduct { get; set; }
}
