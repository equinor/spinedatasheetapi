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
            //Status = tagDataReview.State.MapReviewStatusModelToDto(),
            Id = tagDataReview.Id,
            UserId = tagDataReview.UserId,
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
}
