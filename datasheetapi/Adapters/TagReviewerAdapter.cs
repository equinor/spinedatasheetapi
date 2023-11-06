using datasheetapi.Dtos.TagReviewer;

namespace datasheetapi.Adapters;
public static class TagReviewerAdapter
{
    public static TagReviewerDto ToDto(
        this TagReviewer tagDataReview,
        string? displayName)
    {
        return new TagReviewerDto
        {
            Id = tagDataReview.Id,
            UserId = tagDataReview.UserId,
            State = MapTagReviewerStateModelToDto(tagDataReview.State),
            DisplayName = displayName ?? string.Empty,
            TagNo = tagDataReview.TagNo,
            ContainerReviewId = tagDataReview.ContainerReviewerId,
        };
    }

    public static List<TagReviewerDto> ToDto(
        this ICollection<TagReviewer> tagDataReviews,
        Dictionary<Guid, string> userIdNameMap)
    {
        return tagDataReviews.Select(review => ToDto(review, userIdNameMap[review.UserId])).ToList();
    }

    public static TagReviewer ToModel(this CreateTagReviewerDto tagDataReviewDto)
    {
        return new TagReviewer
        {
            State = TagReviewerStateEnum.NotReviewed,
            UserId = tagDataReviewDto.ReviewerId,
            TagNo = tagDataReviewDto.TagNo
        };
    }

    public static List<TagReviewer> ToModel(this List<CreateTagReviewerDto> tagDataReviewDtos)
    {
        return tagDataReviewDtos.Select(x => ToModel(x)).ToList();
    }

    public static TagReviewerStateEnumDto MapTagReviewerStateModelToDto(TagReviewerStateEnum state)
    {
        return state switch
        {
            TagReviewerStateEnum.NotReviewed => TagReviewerStateEnumDto.NotReviewed,
            TagReviewerStateEnum.Reviewed => TagReviewerStateEnumDto.Reviewed,
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Unknown state: {state}"),
        };
    }

    public static TagReviewerStateEnum MapTagReviewerStateDtoToModel(TagReviewerStateEnumDto state)
    {
        return state switch
        {
            TagReviewerStateEnumDto.NotReviewed => TagReviewerStateEnum.NotReviewed,
            TagReviewerStateEnumDto.Reviewed => TagReviewerStateEnum.Reviewed,
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Unknown state: {state}"),
        };
    }
}
