namespace datasheetapi.Dtos;

public class DatasheetDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string? TagNo { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Area { get; set; }
    public string? Dicipline { get; set; }
    public InstrumentPurchaserRequirement? InstrumentPurchaserRequirement { get; set; }
    public InstrumentSupplierOfferedProduct? InstrumentSupplierOfferedProduct { get; set; }
    public ElectricalPurchaserRequirement? ElectricalPurchaserRequirement { get; set; }
    public ElectricalSupplierOfferedProduct? ElectricalSupplierOfferedProduct { get; set; }
    public MechanicalPurchaserRequirement? MechanicalPurchaserRequirement { get; set; }
    public MechanicalSupplierOfferedProduct? MechanicalSupplierOfferedProduct { get; set; }
}
