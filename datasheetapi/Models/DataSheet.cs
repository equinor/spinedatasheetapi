namespace datasheetapi.Models;

public class Datasheet
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string? TagNo { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Area { get; set; }
    public string? Dicipline { get; set; }
    public PurchaserRequirement? PurchaserRequirement { get; set; }
    public SupplierOfferedProduct? SupplierOfferedProduct { get; set; }
}
