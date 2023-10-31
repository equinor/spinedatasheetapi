namespace datasheetapi.Adapters;
public static class ContainerAdapter
{
    public static ContainerDto? ToDtoOrNull(this Container? revisionContainer)
    {
        if (revisionContainer is null) { return null; }
        return revisionContainer.ToDto();
    }

    private static ContainerDto ToDto(this Container revisionContainer)
    {

        return new ContainerDto
        {
            Id = revisionContainer.Id,
            ContainerName = revisionContainer.ContainerName,
            RevisionNumber = revisionContainer.RevisionNumber,
            ContainerDate = revisionContainer.ContainerDate,
            ContractId = revisionContainer.ContractId,
            TagNos = revisionContainer.Tags.Select(t => t.TagNo).ToList()
        };
    }

    public static List<ContainerDto> ToDto(this List<Container>? revisionContainers)
    {
        if (revisionContainers is null) { return new List<ContainerDto>(); }
        return revisionContainers.Select(ToDto).ToList();
    }

    public static Container? ToModelOrNull(this ContainerDto? revisionContainerDto)
    {
        if (revisionContainerDto is null) { return null; }
        return revisionContainerDto.ToModel();
    }

    private static Container ToModel(this ContainerDto revisionContainerDto)
    {
        return new Container
        {
            Id = revisionContainerDto.Id,
            ContainerName = revisionContainerDto.ContainerName,
            RevisionNumber = revisionContainerDto.RevisionNumber,
            ContainerDate = revisionContainerDto.ContainerDate,
            ContractId = revisionContainerDto.ContractId,
        };
    }

    public static List<Container> ToModel(this List<ContainerDto>? revisionContainerDtos)
    {
        if (revisionContainerDtos is null) { return new List<Container>(); }
        return revisionContainerDtos.Select(ToModel).ToList();
    }
}
