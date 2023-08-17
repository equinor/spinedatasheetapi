namespace datasheetapi.Adapters;
public static class RevisionContainerAdapter
{
    public static RevisionContainerDto? ToDtoOrNull(this RevisionContainer? revisionContainer)
    {
        if (revisionContainer is null) { return null; }
        return revisionContainer.ToDto();
    }

    private static RevisionContainerDto ToDto(this RevisionContainer revisionContainer)
    {

        return new RevisionContainerDto
        {
            Id = revisionContainer.Id,
            CreatedDate = revisionContainer.CreatedDate,
            ModifiedDate = revisionContainer.ModifiedDate,
            RevisionContainerName = revisionContainer.RevisionContainerName,
            RevisionNumber = revisionContainer.RevisionNumber,
            RevisionContainerDate = revisionContainer.RevisionContainerDate,
            RevisionContainerReview = revisionContainer.RevisionContainerReview.ToDtoOrNull(),
            ContractId = revisionContainer.ContractId,
            Contract = null,
        };
    }

    public static List<RevisionContainerDto> ToDto(this List<RevisionContainer>? revisionContainers)
    {
        if (revisionContainers is null) { return new List<RevisionContainerDto>(); }
        return revisionContainers.Select(ToDto).ToList();
    }

    public static RevisionContainer? ToModelOrNull(this RevisionContainerDto? revisionContainerDto)
    {
        if (revisionContainerDto is null) { return null; }
        return revisionContainerDto.ToModel();
    }

    private static RevisionContainer ToModel(this RevisionContainerDto revisionContainerDto)
    {
        return new RevisionContainer
        {
            Id = revisionContainerDto.Id,
            CreatedDate = revisionContainerDto.CreatedDate,
            ModifiedDate = revisionContainerDto.ModifiedDate,
            RevisionContainerName = revisionContainerDto.RevisionContainerName,
            RevisionNumber = revisionContainerDto.RevisionNumber,
            RevisionContainerDate = revisionContainerDto.RevisionContainerDate,
            RevisionContainerReview = revisionContainerDto.RevisionContainerReview.ToModelOrNull(),
            ContractId = revisionContainerDto.ContractId,
            Contract = null,
        };
    }

    public static List<RevisionContainer> ToModel(this List<RevisionContainerDto>? revisionContainerDtos)
    {
        if (revisionContainerDtos is null) { return new List<RevisionContainer>(); }
        return revisionContainerDtos.Select(ToModel).ToList();
    }
}
