namespace datasheetapi.Adapters;
public static class ReviewerAdapter
{
    public static ReviewerDto ToDto(
        this TagReviewer tagDataReview,
        string displayName)
    {
        return new ReviewerDto
        {
            //Status = tagDataReview.State.MapReviewStatusModelToDto(),
            ReviewerId = tagDataReview.ReviewerId,
            CreatedDate = tagDataReview.CreatedDate,
            ModifiedDate = tagDataReview.ModifiedDate,
            DisplayName = displayName,
        };
    }

    public static List<ReviewerDto> ToDto(
        this List<TagReviewer> tagDataReviews,
        Dictionary<Guid, string> userIdNameMap)
    {
        return tagDataReviews.Select(review => ToDto(review, userIdNameMap[review.ReviewerId])).ToList();
    }

    public static TagReviewer ToModel(this CreateReviewerDto tagDataReviewDto)
    {
        return new TagReviewer
        {
            State = TagReviewerStateEnum.NotReviewed,
            ReviewerId = tagDataReviewDto.ReviewerId,
        };
    }

    public static List<TagReviewer> ToModel(this List<CreateReviewerDto> tagDataReviewDtos)
    {
        return tagDataReviewDtos.Select(x => ToModel(x)).ToList();
    }
}
