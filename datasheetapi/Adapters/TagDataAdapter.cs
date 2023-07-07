namespace datasheetapi.Adapters;
public static class TagDataAdapter
{
    public static ITagDataDto? ToDtoOrNull(this ITagData? tagData)
    {
        if (tagData is null) { return null; }
        return tagData.ToDto();
    }

    public static ITagDataDto ToDto(this ITagData tagData)
    {
        if (tagData is InstrumentTagData instrumentTagData)
        {
            return InstrumentToDto(instrumentTagData);
        }
        if (tagData is ElectricalTagData electricalTagData)
        {
            return ElectricalToDto(electricalTagData);
        }
        if (tagData is MechanicalTagData mechanicalTagData)
        {
            return MechanicalToDto(mechanicalTagData);
        }
        return DefaultToDto<TagDataDto>(tagData);
    }

    public static List<ITagDataDto> ToDto(this List<ITagData>? tagData)
    {
        if (tagData is null) { return new List<ITagDataDto>(); }
        return tagData.Select(x => x.ToDto()).ToList();
    }

    public static List<ITagDataDto> ToDto(this List<TagData>? tagData)
    {
        if (tagData is null) { return new List<ITagDataDto>(); }
        return tagData.Select(x => x.ToDto()).ToList();
    }

    private static ITagDataDto InstrumentToDto(InstrumentTagData tagData)
    {
        var dto = DefaultToDto<InstrumentTagDataDto>(tagData);
        dto.InstrumentPurchaserRequirement = tagData.InstrumentPurchaserRequirement;
        dto.InstrumentSupplierOfferedProduct = tagData.InstrumentSupplierOfferedProduct;
        return dto;
    }

    private static ITagDataDto ElectricalToDto(ElectricalTagData tagData)
    {
        var dto = DefaultToDto<ElectricalTagDataDto>(tagData);
        dto.ElectricalPurchaserRequirement = tagData.ElectricalPurchaserRequirement;
        dto.ElectricalSupplierOfferedProduct = tagData.ElectricalSupplierOfferedProduct;
        return dto;
    }

    private static ITagDataDto MechanicalToDto(MechanicalTagData tagData)
    {
        var dto = DefaultToDto<MechanicalTagDataDto>(tagData);
        dto.MechanicalPurchaserRequirement = tagData.MechanicalPurchaserRequirement;
        dto.MechanicalSupplierOfferedProduct = tagData.MechanicalSupplierOfferedProduct;
        return dto;
    }

    private static T DefaultToDto<T>(ITagData tagData)
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
            Version = tagData.Version,
            Review = tagData.TagDataReview.ToDtoOrNull(),
            RevisionContainer = null,
        };
    }
}
