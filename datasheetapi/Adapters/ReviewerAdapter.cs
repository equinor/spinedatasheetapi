namespace datasheetapi.Adapters;
public static class ReviewerAdapter
{
    public static ReviewerDto ToDto(
        this Reviewer tagDataReview,
        string displayName)
    {
        return new ReviewerDto
        {
            Status = tagDataReview.Status.MapReviewStatusModelToDto(),
            ReviewerId = tagDataReview.ReviewerId,
            TagDataReviewId = tagDataReview.TagDataReviewId,
            CreatedDate = tagDataReview.CreatedDate,
            ModifiedDate = tagDataReview.ModifiedDate,
            DisplayName = displayName,
        };
    }

    public static List<ReviewerDto> ToDto(
        this List<Reviewer> tagDataReviews,
        Dictionary<Guid, string> userIdNameMap)
    {
        return tagDataReviews.Select(review => ToDto(review, userIdNameMap[review.ReviewerId])).ToList();
    }

    public static Reviewer ToModel(this CreateReviewerDto tagDataReviewDto)
    {
        return new Reviewer
        {
            Status = ReviewStatusEnum.New,
            ReviewerId = tagDataReviewDto.ReviewerId,
        };
    }

    public static List<Reviewer> ToModel(this List<CreateReviewerDto> tagDataReviewDtos)
    {
        return tagDataReviewDtos.Select(x => ToModel(x)).ToList();
    }
}
