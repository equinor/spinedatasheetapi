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
            RevisionContainerName = revisionContainer.RevisionContainerName,
            RevisionNumber = revisionContainer.RevisionNumber,
            RevisionContainerDate = revisionContainer.RevisionContainerDate,
            ContractId = revisionContainer.ContractId,
            Contract = null,
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
            RevisionContainerName = revisionContainerDto.RevisionContainerName,
            RevisionNumber = revisionContainerDto.RevisionNumber,
            RevisionContainerDate = revisionContainerDto.RevisionContainerDate,
            ContractId = revisionContainerDto.ContractId,
        };
    }

    public static List<Container> ToModel(this List<ContainerDto>? revisionContainerDtos)
    {
        if (revisionContainerDtos is null) { return new List<Container>(); }
        return revisionContainerDtos.Select(ToModel).ToList();
    }
}
