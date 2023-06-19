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
        var tagDataDtos = new List<ITagDataDto>();
        var allTagData = await _dummyFAMService.GetTagData();
        foreach (var tagData in allTagData)
        {
            tagDataDtos.Add(MapTagDataToTagDataDto(tagData));
        }

        return tagDataDtos;
    }

    public async Task<List<ITagDataDto>> GetTagDataForProject(Guid id)
    {
        var tagDataDtos = new List<ITagDataDto>();
        var tagDataForProject = await _dummyFAMService.GetTagDataForProject(id);
        foreach (var tagData in tagDataForProject)
        {
            tagDataDtos.Add(MapTagDataToTagDataDto(tagData));
        }

        return tagDataDtos;
    }

    private static ITagDataDto MapTagDataToTagDataDto(ITagData tagData)
    {
        if (tagData is InstrumentTagData instrumentTagData)
        {
            return MapInstrumentTagDataToTagDataDto(instrumentTagData);
        }
        if (tagData is ElectricalTagData electricalTagData)
        {
            return MapElectricalTagDataToTagDataDto(electricalTagData);
        }
        if (tagData is MechanicalTagData mechanicalTagData)
        {
            return MapMechanicalTagDataToTagDataDto(mechanicalTagData);
        }
        return MapDefaultTagDataToTagDataDto<TagDataDto>(tagData);
    }

    private static ITagDataDto MapInstrumentTagDataToTagDataDto(InstrumentTagData tagData)
    {
        var dto = MapDefaultTagDataToTagDataDto<InstrumentTagDataDto>(tagData);
        dto.InstrumentPurchaserRequirement = tagData.InstrumentPurchaserRequirement;
        dto.InstrumentSupplierOfferedProduct = tagData.InstrumentSupplierOfferedProduct;
        return dto;
    }

    private static ITagDataDto MapElectricalTagDataToTagDataDto(ElectricalTagData tagData)
    {
        var dto = MapDefaultTagDataToTagDataDto<ElectricalTagDataDto>(tagData);
        dto.ElectricalPurchaserRequirement = tagData.ElectricalPurchaserRequirement;
        dto.ElectricalSupplierOfferedProduct = tagData.ElectricalSupplierOfferedProduct;
        return dto;
    }

    private static ITagDataDto MapMechanicalTagDataToTagDataDto(MechanicalTagData tagData)
    {
        var dto = MapDefaultTagDataToTagDataDto<MechanicalTagDataDto>(tagData);
        dto.MechanicalPurchaserRequirement = tagData.MechanicalPurchaserRequirement;
        dto.MechanicalSupplierOfferedProduct = tagData.MechanicalSupplierOfferedProduct;
        return dto;
    }

    private static T MapDefaultTagDataToTagDataDto<T>(ITagData tagData)
        where T : class, ITagDataDto, new()
    {
        return new T
        {
            Id = tagData.Id,
            TagNo = tagData.TagNo,
            Area = tagData.Area,
            Category = tagData.Category,
            Description = tagData.Description,
            Discipline = tagData.Discipline,
            ProjectId = tagData.ProjectId,
        };
    }
}
