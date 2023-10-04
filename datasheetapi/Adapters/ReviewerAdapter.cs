namespace datasheetapi.Adapters;
public static class ReviewerAdapter
{
    public static ReviewerDto? ToDtoOrNull(this Reviewer? tagDataReview)
    {
        if (tagDataReview is null) { return null; }
        return tagDataReview.ToDto();
    }

    private static ReviewerDto ToDto(this Reviewer tagDataReview)
    {
        return new ReviewerDto
        {
            Status = tagDataReview.Status.MapReviewStatusModelToDto(),
            ReviewerId = tagDataReview.ReviewerId,
            TagDataReviewId = tagDataReview.TagDataReviewId,
            CreatedDate = tagDataReview.CreatedDate,
            ModifiedDate = tagDataReview.ModifiedDate,
        };
    }

    public static List<ReviewerDto> ToDto(this List<Reviewer>? tagDataReviews)
    {
        if (tagDataReviews is null) { return new List<ReviewerDto>(); }
        return tagDataReviews.Select(ToDto).ToList();
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
