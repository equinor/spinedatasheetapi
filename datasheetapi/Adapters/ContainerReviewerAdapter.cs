using datasheetapi.Dtos.ContainerReview;
using datasheetapi.Dtos.ContainerReviewer;
using datasheetapi.Dtos.TagReviewer;

namespace datasheetapi.Adapters;

public static class ContainerReviewerAdapter
{
    public static ContainerReviewer ToModel(this CreateContainerReviewerDto dto)
    {
        return new ContainerReviewer
        {
            UserId = dto.UserId,
            State = ContainerReviewerStateEnum.Open,
            TagReviewers = dto.TagReviewers?.ToModel() ?? new List<TagReviewer>()
        };
    }

    public static ContainerReviewerDto ToDto(this ContainerReviewer model, Dictionary<Guid, string> userIdNameMap)
    {
        return new ContainerReviewerDto
        {
            Id = model.Id,
            ContainerReviewId = model.ContainerReviewId,
            State = MapContainerReviewStateEnumToDto(model.State),
            UserId = model.UserId,
            TagReviewers = model.TagReviewers.ToDto(userIdNameMap),
        };
    }

    public static ContainerReviewerStateEnumDto MapContainerReviewStateEnumToDto(ContainerReviewerStateEnum state)
    {
        return state switch
        {
            ContainerReviewerStateEnum.Open => ContainerReviewerStateEnumDto.Open,
            ContainerReviewerStateEnum.Abandoned => ContainerReviewerStateEnumDto.Abandoned,
            ContainerReviewerStateEnum.Complete => ContainerReviewerStateEnumDto.Complete,
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Unknown state: {state}"),
        };
    }
}
