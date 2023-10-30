using datasheetapi.Dtos.ContainerReview;

namespace datasheetapi.Adapters;
public static class ContainerReviewAdapter
{
    public static ContainerReviewDto? ToDtoOrNull(this ContainerReview? revisionContainerReview)
    {
        if (revisionContainerReview is null) { return null; }
        return revisionContainerReview.ToDto();
    }

    public static ContainerReviewDto ToDto(this ContainerReview revisionContainerReview)
    {
        return new ContainerReviewDto
        {
            Id = revisionContainerReview.Id,
            //Status = revisionContainerReview.Status,
            CommentResponsible = revisionContainerReview.CommentResponsible,
            ContainerId = revisionContainerReview.ContainerId,
        };
    }

    public static List<ContainerReviewDto> ToDto(this List<ContainerReview>? revisionContainerReviews)
    {
        if (revisionContainerReviews is null) { return new List<ContainerReviewDto>(); }
        return revisionContainerReviews.Select(ToDto).ToList();
    }

    public static ContainerReview? ToModelOrNull(this ContainerReviewDto? revisionContainerReviewDto)
    {
        if (revisionContainerReviewDto is null) { return null; }
        return revisionContainerReviewDto.ToModel();
    }

    public static ContainerReview ToModel(this ContainerReviewDto revisionContainerReviewDto)
    {
        return new ContainerReview
        {
            Id = revisionContainerReviewDto.Id,
            //Status = revisionContainerReviewDto.Status,
            CommentResponsible = revisionContainerReviewDto.CommentResponsible,
            ContainerId = revisionContainerReviewDto.ContainerId,
        };
    }

    public static ContainerReview ToModel(this CreateContainerReviewDto dto)
    {
        return new ContainerReview
        {
            ContainerId = dto.RevisionContainerId,
            //Status = dto.Status.MapReviewStatusDtoToModel(),
        };
    }
}
