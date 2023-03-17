namespace datasheetapi.Models;

public class DataSheet
{
    public Guid Id { get; set; }
    public PurchaserRequirement PurchaserRequirement { get; set; }
    public SupplierOfferedProduct SupplierOfferedProduct { get; set; }
}
