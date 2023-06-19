namespace datasheetapi.Services;

public class TagDataService : ITagDataService
{
    private readonly IDummyFAMService _dummyFAMService;
    public TagDataService(IDummyFAMService dummyFAMService)
    {
        _dummyFAMService = dummyFAMService;
    }

    public async Task<ITagDataDto?> GetTagDataById(Guid id)
    {
        var tagData = await _dummyFAMService.GetTagData(id);

        if (tagData == null)
        {
            return null;
        }

        return MapTagDataToTagDataDto(tagData);
    }

    public async Task<List<ITagDataDto>> GetAllTagData()
    {
        var dataSheetsDto = new List<ITagDataDto>();
        var datasheets = await _dummyFAMService.GetTagData();
        foreach (var dataSheet in datasheets)
        {
            dataSheetsDto.Add(MapTagDataToTagDataDto(dataSheet));
        }

        return dataSheetsDto;
    }

    public async Task<ActionResult<List<ITagDataDto>>> GetTagDataForProject(Guid id)
    {
        var dataSheetsDto = new List<ITagDataDto>();
        var datasheets = await _dummyFAMService.GetTagDataForProject(id);
        foreach (var dataSheet in datasheets)
        {
            dataSheetsDto.Add(MapTagDataToTagDataDto(dataSheet));
        }

        return dataSheetsDto;
    }

    private ITagDataDto MapTagDataToTagDataDto(ITagData tagData)
    {
        if (tagData is InstrumentTagData)
        {
            return MapInstrumentTagDataToTagDataDto(tagData as InstrumentTagData);
        }
        if (tagData is ElectricalTagData)
        {
            return MapElectricalTagDataToTagDataDto(tagData as ElectricalTagData);
        }
        if (tagData is MechanicalTagData)
        {
            return MapMechanicalTagDataToTagDataDto(tagData as MechanicalTagData);
        }
        return MapDatasheetToDto(tagData);
    }

    private static ITagDataDto MapInstrumentTagDataToTagDataDto(InstrumentTagData tagData)
    {
        return new InstrumentTagDataDto
        {
            Id = tagData.Id,
            TagNo = tagData.TagNo,
            Area = tagData.Area,
            Category = tagData.Category,
            Description = tagData.Description,
            Discipline = tagData.Discipline,
            ProjectId = tagData.ProjectId,
            InstrumentPurchaserRequirement = tagData.InstrumentPurchaserRequirement,
            InstrumentSupplierOfferedProduct = tagData.InstrumentSupplierOfferedProduct,
        };
    }

    private static ITagDataDto MapElectricalTagDataToTagDataDto(ElectricalTagData tagData)
    {
        return new ElectricalTagDataDto
        {
            Id = tagData.Id,
            TagNo = tagData.TagNo,
            Area = tagData.Area,
            Category = tagData.Category,
            Description = tagData.Description,
            Discipline = tagData.Discipline,
            ProjectId = tagData.ProjectId,
            ElectricalPurchaserRequirement = tagData.ElectricalPurchaserRequirement,
            ElectricalSupplierOfferedProduct = tagData.ElectricalSupplierOfferedProduct,
        };
    }

    private static ITagDataDto MapMechanicalTagDataToTagDataDto(MechanicalTagData tagData)
    {
        return new MechanicalTagDataDto
        {
            Id = tagData.Id,
            TagNo = tagData.TagNo,
            Area = tagData.Area,
            Category = tagData.Category,
            Description = tagData.Description,
            Discipline = tagData.Discipline,
            ProjectId = tagData.ProjectId,
            MechanicalPurchaserRequirement = tagData.MechanicalPurchaserRequirement,
            MechanicalSupplierOfferedProduct = tagData.MechanicalSupplierOfferedProduct,
        };
    }

    private static TagDataDto MapDatasheetToDto(ITagData dataSheet)
    {
        return new TagDataDto
        {
            Id = dataSheet.Id,
            TagNo = dataSheet.TagNo,
            Area = dataSheet.Area,
            Category = dataSheet.Category,
            Description = dataSheet.Description,
            Discipline = dataSheet.Discipline,
            ProjectId = dataSheet.ProjectId,
        };
    }
}
