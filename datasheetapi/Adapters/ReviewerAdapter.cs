using datasheetapi.Dtos.TagReviewer;

namespace datasheetapi.Adapters;
public static class ReviewerAdapter
{
    public static TagReviewerDto ToDto(
        this TagReviewer tagDataReview,
        string displayName)
    {
        return new TagReviewerDto
        {
            //Status = tagDataReview.State.MapReviewStatusModelToDto(),
            UserId = tagDataReview.UserId,
            DisplayName = displayName,
        };
    }

    public static List<TagReviewerDto> ToDto(
        this List<TagReviewer> tagDataReviews,
        Dictionary<Guid, string> userIdNameMap)
    {
        return tagDataReviews.Select(review => ToDto(review, userIdNameMap[review.UserId])).ToList();
    }

    public static TagReviewer ToModel(this CreateReviewerDto tagDataReviewDto)
    {
        return new TagReviewer
        {
            State = TagReviewerStateEnum.NotReviewed,
            UserId = tagDataReviewDto.ReviewerId,
        };
    }

    public static List<TagReviewer> ToModel(this List<CreateReviewerDto> tagDataReviewDtos)
    {
        return tagDataReviewDtos.Select(x => ToModel(x)).ToList();
    }
}
