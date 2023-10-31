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
            State = MapContainerReviewStateModelToDto(model.State),
            UserId = model.UserId,
            TagReviewers = model.TagReviewers.ToDto(userIdNameMap),
        };
    }

    public static List<ContainerReviewerDto> ToDto(this List<ContainerReviewer> model,
        Dictionary<Guid, string> userIdNameMap)
    {
        return model.Select(m => m.ToDto(userIdNameMap)).ToList();
    }

    public static ContainerReviewerStateEnumDto MapContainerReviewStateModelToDto(ContainerReviewerStateEnum state)
    {
        return state switch
        {
            ContainerReviewerStateEnum.Open => ContainerReviewerStateEnumDto.Open,
            ContainerReviewerStateEnum.Abandoned => ContainerReviewerStateEnumDto.Abandoned,
            ContainerReviewerStateEnum.Complete => ContainerReviewerStateEnumDto.Complete,
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Unknown state: {state}"),
        };
    }

    public static ContainerReviewerStateEnum MapContainerReviewStateDtoToModel(ContainerReviewerStateEnumDto state)
    {
        return state switch
        {
            ContainerReviewerStateEnumDto.Open => ContainerReviewerStateEnum.Open,
            ContainerReviewerStateEnumDto.Abandoned => ContainerReviewerStateEnum.Abandoned,
            ContainerReviewerStateEnumDto.Complete => ContainerReviewerStateEnum.Complete,
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Unknown state: {state}"),
        };
    }
}
