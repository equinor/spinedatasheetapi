namespace datasheetapi.Adapters;
public static class TagDataReviewAdapter
{
    public static TagDataReviewDto? ToDtoOrNull(this TagDataReview? tagDataReview)
    {
        if (tagDataReview is null) { return null; }
        return tagDataReview.ToDto();
    }

    private static TagDataReviewDto ToDto(this TagDataReview tagDataReview)
    {
        return new TagDataReviewDto
        {
            Id = tagDataReview.Id,
            CreatedDate = tagDataReview.CreatedDate,
            ModifiedDate = tagDataReview.ModifiedDate,
            TagNo = tagDataReview.TagNo,
            Status = tagDataReview.Status.MapReviewStatusModelToDto(),
            ApproverId = tagDataReview.ApproverId,
            CommentResponsible = tagDataReview.CommentResponsible,
            Approved = tagDataReview.Approved,
            TagDataVersion = tagDataReview.TagDataVersion,
        };
    }

    public static ReviewStatusDto MapReviewStatusModelToDto(this ReviewStatusEnum model)
    {
        return model switch
        {
            ReviewStatusEnum.New => ReviewStatusDto.New,
            ReviewStatusEnum.Reviewed => ReviewStatusDto.Reviewed,
            ReviewStatusEnum.Resubmit => ReviewStatusDto.Resubmit,
            ReviewStatusEnum.Diff => ReviewStatusDto.Diff,
            ReviewStatusEnum.Duplicate => ReviewStatusDto.Duplicate,
            ReviewStatusEnum.ReviewedWithComment => ReviewStatusDto.ReviewedWithComment,
            ReviewStatusEnum.NotReviewed => ReviewStatusDto.NotReviewed,
            ReviewStatusEnum.Deleted => ReviewStatusDto.Deleted,
            _ => throw new ArgumentOutOfRangeException(nameof(model), $"Unknown status: {model}"),
        };
    }

    public static ReviewStatusEnum MapReviewStatusDtoToModel(this ReviewStatusDto dto)
    {
        return dto switch
        {
            ReviewStatusDto.New => ReviewStatusEnum.New,
            ReviewStatusDto.Reviewed => ReviewStatusEnum.Reviewed,
            ReviewStatusDto.Resubmit => ReviewStatusEnum.Resubmit,
            ReviewStatusDto.Diff => ReviewStatusEnum.Diff,
            ReviewStatusDto.Duplicate => ReviewStatusEnum.Duplicate,
            ReviewStatusDto.ReviewedWithComment => ReviewStatusEnum.ReviewedWithComment,
            ReviewStatusDto.NotReviewed => ReviewStatusEnum.NotReviewed,
            ReviewStatusDto.Deleted => ReviewStatusEnum.Deleted,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), $"Unknown status: {dto}"),
        };
    }

    public static List<TagDataReviewDto> ToDto(this List<TagDataReview>? tagDataReviews)
    {
        if (tagDataReviews is null) { return new List<TagDataReviewDto>(); }
        return tagDataReviews.Select(ToDto).ToList();
    }

    public static TagDataReview ToModel(this CreateTagDataReviewDto tagDataReviewDto)
    {
        var model = new TagDataReview
        {
            TagNo = tagDataReviewDto.TagNo,
            Status = tagDataReviewDto.Status.MapReviewStatusDtoToModel(),
            ReviewerReviews = new List<ReviewerTagDataReview>()
        };

        var reviewerTagDataReview = tagDataReviewDto.ReviewerTagDataReview?.ToModel();
        if (reviewerTagDataReview != null)
        {
            model.ReviewerReviews.Add(reviewerTagDataReview);
        }

        return model;
    }
}
