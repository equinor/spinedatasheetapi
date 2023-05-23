namespace datasheetapi.Services;

public class DatasheetService : IDatasheetService
{
    private readonly IDummyFAMService _dummyFAMService;
    public DatasheetService(IDummyFAMService dummyFAMService)
    {
        _dummyFAMService = dummyFAMService;
    }

    public async Task<DatasheetDto?> GetDatasheetById(Guid id)
    {
        var dataSheet = await _dummyFAMService.GetDatasheet(id);

        if (dataSheet == null)
        {
            return null;
        }

        return MapDatasheetToDto(dataSheet);
    }

    public async Task<List<DatasheetDto>> GetAllDatasheets()
    {
        var dataSheetsDto = new List<DatasheetDto>();
        var datasheets = await _dummyFAMService.GetDatasheets();
        foreach (var dataSheet in datasheets)
        {
            dataSheetsDto.Add(MapDatasheetToDto(dataSheet));
        }

        return dataSheetsDto;
    }

    public async Task<ActionResult<List<DatasheetDto>>> GetDatasheetsForProject(Guid id)
    {
        var dataSheetsDto = new List<DatasheetDto>();
        var datasheets = await _dummyFAMService.GetDatasheetsForProject(id);
        foreach (var dataSheet in datasheets)
        {
            dataSheetsDto.Add(MapDatasheetToDto(dataSheet));
        }

        return dataSheetsDto;
    }

    private Datasheet MapDtoToDatasheet(DatasheetDto dataSheetDto, Datasheet? dataSheet = null)
    {
        if (dataSheet == null)
        {
            dataSheet = new Datasheet();
        }

        dataSheet.Id = dataSheetDto.Id;
        dataSheet.PurchaserRequirement = dataSheetDto.PurchaserRequirement;
        dataSheet.SupplierOfferedProduct = dataSheetDto.SupplierOfferedProduct;
        dataSheet.ElectricalPurchaserRequirement = dataSheetDto.ElectricalPurchaserRequirement;
        dataSheet.ElectricalSupplierOfferedProduct = dataSheetDto.ElectricalSupplierOfferedProduct;

        return dataSheet;
    }

    private static DatasheetDto MapDatasheetToDto(Datasheet dataSheet)
    {
        return new DatasheetDto
        {
            Id = dataSheet.Id,
            TagNo = dataSheet.TagNo,
            Area = dataSheet.Area,
            Category = dataSheet.Category,
            Description = dataSheet.Description,
            Dicipline = dataSheet.Dicipline,
            ProjectId = dataSheet.ProjectId,
            PurchaserRequirement = dataSheet.PurchaserRequirement,
            SupplierOfferedProduct = dataSheet.SupplierOfferedProduct,
            ElectricalPurchaserRequirement = dataSheet.ElectricalPurchaserRequirement,
            ElectricalSupplierOfferedProduct = dataSheet.ElectricalSupplierOfferedProduct,
        };
    }
}
