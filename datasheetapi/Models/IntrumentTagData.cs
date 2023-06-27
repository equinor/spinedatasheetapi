namespace datasheetapi.Models;

public class InstrumentTagData : TagData
{
    public InstrumentTagData(Package package) : base(package)
    {
    }

    public InstrumentPurchaserRequirement? InstrumentPurchaserRequirement { get; set; }
    public InstrumentSupplierOfferedProduct? InstrumentSupplierOfferedProduct { get; set; }
}
