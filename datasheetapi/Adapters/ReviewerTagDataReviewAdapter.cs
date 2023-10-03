namespace datasheetapi.Adapters;
public static class ReviewerTagDataReviewAdapter
{
    public static ReviewerTagDataReviewDto? ToDtoOrNull(this ReviewerTagDataReview? tagDataReview)
    {
        if (tagDataReview is null) { return null; }
        return tagDataReview.ToDto();
    }

    private static ReviewerTagDataReviewDto ToDto(this ReviewerTagDataReview tagDataReview)
    {
        return new ReviewerTagDataReviewDto
        {
            Status = tagDataReview.Status.MapReviewStatusModelToDto(),
            ReviewerId = tagDataReview.ReviewerId,
            TagDataReviewId = tagDataReview.TagDataReviewId,
        };
    }

    public static ReviewerTagDataReview ToModel(this CreateReviewerTagDataReviewDto tagDataReviewDto)
    {
        return new ReviewerTagDataReview
        {
            Status = tagDataReviewDto.Status.MapReviewStatusDtoToModel(),
            ReviewerId = tagDataReviewDto.ReviewerId,
        };
    }
}
