namespace datasheetapi.Dtos;

public record InstrumentTagDataDto : TagDataDto
{
    public InstrumentPurchaserRequirement? InstrumentPurchaserRequirement { get; set; }
    public InstrumentSupplierOfferedProduct? InstrumentSupplierOfferedProduct { get; set; }
}
