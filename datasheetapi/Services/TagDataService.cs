using datasheetapi.Models.Electrical;
using datasheetapi.Models.Instrument;
using datasheetapi.Models.Mechanical;

namespace datasheetapi.Services;

public class TagDataService : ITagDataService
{
    private readonly IDummyFAMService _dummyFAMService;
    public TagDataService(IDummyFAMService dummyFAMService)
    {
        _dummyFAMService = dummyFAMService;
    }

    public async Task<TagDataDto?> GetDatasheetById(Guid id)
    {
        var tagData = await _dummyFAMService.GetDatasheet(id);

        if (tagData == null)
        {
            return null;
        }

        return MapDatasheetToDto(tagData);
    }

    public async Task<List<TagDataDto>> GetAllDatasheets()
    {
        var dataSheetsDto = new List<TagDataDto>();
        var datasheets = await _dummyFAMService.GetDatasheets();
        foreach (var dataSheet in datasheets)
        {
            dataSheetsDto.Add(MapDatasheetToDto(dataSheet));
        }

        return dataSheetsDto;
    }

    public async Task<ActionResult<List<TagDataDto>>> GetDatasheetsForProject(Guid id)
    {
        var dataSheetsDto = new List<TagDataDto>();
        var datasheets = await _dummyFAMService.GetDatasheetsForProject(id);
        foreach (var dataSheet in datasheets)
        {
            dataSheetsDto.Add(MapDatasheetToDto(dataSheet));
        }

        return dataSheetsDto;
    }

    private TagData MapDtoToDatasheet(TagDataDto tagDataDto, TagData? tagData = null)
    {
        if (tagData == null)
        {
            tagData = new TagData();
        }

        tagData.Id = tagDataDto.Id;
        tagData.InstrumentPurchaserRequirement = tagDataDto.InstrumentPurchaserRequirement;
        tagData.InstrumentSupplierOfferedProduct = tagDataDto.InstrumentSupplierOfferedProduct;
        tagData.ElectricalPurchaserRequirement = tagDataDto.ElectricalPurchaserRequirement;
        tagData.ElectricalSupplierOfferedProduct = tagDataDto.ElectricalSupplierOfferedProduct;
        tagData.MechanicalPurchaserRequirement = tagDataDto.MechanicalPurchaserRequirement;
        tagData.MechanicalSupplierOfferedProduct = tagDataDto.MechanicalSupplierOfferedProduct;

        return tagData;
    }

    private static TagDataDto MapDatasheetToDto(TagData tagData)
    {
        return new TagDataDto
        {
            Id = tagData.Id,
            TagNo = tagData.TagNo,
            Area = tagData.Area,
            Category = tagData.Category,
            Description = tagData.Description,
            Dicipline = tagData.Dicipline,
            ProjectId = tagData.ProjectId,
        };
    }

    private static InstrumentTagDataDto MapInstrumentTagDataToDto(InstrumentTagData instrumentTagData)
    {
        var tagData = MapDatasheetToDto(instrumentTagData);
        tagData.InstrumentPurchaserRequirement = instrumentTagData.InstrumentPurchaserRequirement;
        tagData.InstrumentSupplierOfferedProduct = instrumentTagData.InstrumentSupplierOfferedProduct;

        return tagData;
    }

    private static InstrumentTagDataDto MapElectricalTagDataToDto(ElectricalTagData electricalTagData)
    {
        var tagData = MapDatasheetToDto(electricalTagData);
        tagData.ElectricalPurchaserRequirement = electricalTagData.ElectricalPurchaserRequirement;
        tagData.ElectricalSupplierOfferedProduct = electricalTagData.ElectricalSupplierOfferedProduct;

        return tagData;
    }

    private static InstrumentTagDataDto MapMechanicalTagDataToDto(MechanicalTagData mechanicalTagData)
    {
        var tagData = MapDatasheetToDto(mechanicalTagData);
        tagData.InstrumentPurchaserRequirement = mechanicalTagData.MechanicalPurchaserRequirement;
        tagData.InstrumentSupplierOfferedProduct = mechanicalTagData.MechanicalSupplierOfferedProduct;

        return tagData;
    }
}
