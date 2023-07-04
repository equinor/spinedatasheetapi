namespace datasheetapi.Models;

public class InstrumentTagData : TagData
{
    public InstrumentTagData() : base()
    {
    }

    public InstrumentPurchaserRequirement? InstrumentPurchaserRequirement { get; set; }
    public InstrumentSupplierOfferedProduct? InstrumentSupplierOfferedProduct { get; set; }
}
