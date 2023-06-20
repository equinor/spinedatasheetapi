namespace datasheetapi.Dtos
{
    public interface IInstrumentTagDataDto : ITagDataDto
    {
        InstrumentPurchaserRequirement? InstrumentPurchaserRequirement { get; set; }
        InstrumentSupplierOfferedProduct? InstrumentSupplierOfferedProduct { get; set; }
    }
}
