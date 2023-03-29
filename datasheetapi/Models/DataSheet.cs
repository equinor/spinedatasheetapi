namespace datasheetapi.Models;

public class Datasheet
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public PurchaserRequirement? PurchaserRequirement { get; set; }
    public SupplierOfferedProduct? SupplierOfferedProduct { get; set; }
}
