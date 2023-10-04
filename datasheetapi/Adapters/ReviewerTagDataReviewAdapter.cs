namespace datasheetapi.Adapters;
public static class ReviewerTagDataReviewAdapter
{
    public static ReviewerTagDataReviewDto? ToDtoOrNull(this Reviewer? tagDataReview)
    {
        if (tagDataReview is null) { return null; }
        return tagDataReview.ToDto();
    }

    private static ReviewerTagDataReviewDto ToDto(this Reviewer tagDataReview)
    {
        return new ReviewerTagDataReviewDto
        {
            Status = tagDataReview.Status.MapReviewStatusModelToDto(),
            ReviewerId = tagDataReview.ReviewerId,
            TagDataReviewId = tagDataReview.TagDataReviewId,
        };
    }

    public static Reviewer ToModel(this CreateReviewerTagDataReviewDto tagDataReviewDto)
    {
        return new Reviewer
        {
            Status = tagDataReviewDto.Status.MapReviewStatusDtoToModel(),
            ReviewerId = tagDataReviewDto.ReviewerId,
        };
    }

    public static List<Reviewer> ToModel(this List<CreateReviewerTagDataReviewDto> tagDataReviewDtos)
    {
        return tagDataReviewDtos.Select(x =>  ToModel(x)).ToList();
    }
}
