using datasheetapi.Adapters;

namespace datasheetapi;
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
            TagData = revisionContainer.TagData.ToDto(),
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
}
