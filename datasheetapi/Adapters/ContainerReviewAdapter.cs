using datasheetapi.Dtos.ContainerReview;

namespace datasheetapi.Adapters;
public static class ContainerReviewAdapter
{
    public static ContainerReviewDto ToDto(this ContainerReview revisionContainerReview)
    {
        return new ContainerReviewDto
        {
            Id = revisionContainerReview.Id,
            State = MapContainerReviewStateModelToDto(revisionContainerReview.State),
            CommentResponsible = revisionContainerReview.CommentResponsible,
            ContainerId = revisionContainerReview.ContainerId,
        };
    }

    public static List<ContainerReviewDto> ToDto(this List<ContainerReview>? revisionContainerReviews)
    {
        if (revisionContainerReviews is null) { return new List<ContainerReviewDto>(); }
        return revisionContainerReviews.Select(ToDto).ToList();
    }

    public static ContainerReview ToModel(this ContainerReviewDto revisionContainerReviewDto)
    {
        return new ContainerReview
        {
            Id = revisionContainerReviewDto.Id,
            State = MapContainerReviewStateDtoToModel(revisionContainerReviewDto.State),
            CommentResponsible = revisionContainerReviewDto.CommentResponsible,
            ContainerId = revisionContainerReviewDto.ContainerId,
        };
    }

    public static ContainerReview ToModel(this CreateContainerReviewDto dto)
    {
        return new ContainerReview
        {
            ContainerId = dto.RevisionContainerId,
            State = MapContainerReviewStateDtoToModel(dto.State),
        };
    }

    public static ContainerReviewStateEnumDto MapContainerReviewStateModelToDto(ContainerReviewStateEnum state)
    {
        return state switch
        {
            ContainerReviewStateEnum.Active => ContainerReviewStateEnumDto.Active,
            ContainerReviewStateEnum.SentToContractor => ContainerReviewStateEnumDto.SentToContractor,
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Unknown state: {state}"),
        };
    }

    public static ContainerReviewStateEnum MapContainerReviewStateDtoToModel(ContainerReviewStateEnumDto state)
    {
        return state switch
        {
            ContainerReviewStateEnumDto.Active => ContainerReviewStateEnum.Active,
            ContainerReviewStateEnumDto.SentToContractor => ContainerReviewStateEnum.SentToContractor,
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Unknown state: {state}"),
        };
    }
}
