namespace datasheetapi.Dtos;

public class DatasheetDto
{
    public Guid Id { get; set; }
    public PurchaserRequirement? PurchaserRequirement { get; set; }
    public SupplierOfferedProduct? SupplierOfferedProduct { get; set; }
}
