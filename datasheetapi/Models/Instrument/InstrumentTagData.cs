namespace datasheetapi.Models.Instrument;

public class InstrumentTagData: TagData
{
    public InstrumentPurchaserRequirement? InstrumentPurchaserRequirement { get; set; }
    public InstrumentSupplierOfferedProduct? InstrumentSupplierOfferedProduct { get; set; }
}