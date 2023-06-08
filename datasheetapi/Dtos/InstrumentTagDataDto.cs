namespace datasheetapi.Dtos;

public class InstrumentTagDataDto: TagDataDto
{
    public InstrumentPurchaserRequirement? InstrumentPurchaserRequirement { get; set; }
    public InstrumentSupplierOfferedProduct? InstrumentSupplierOfferedProduct { get; set; }
}
