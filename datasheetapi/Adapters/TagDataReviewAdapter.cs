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
            Status = tagDataReview.Status,
            ApproverId = tagDataReview.ApproverId,
            CommentResponsible = tagDataReview.CommentResponsible,
            Approved = tagDataReview.Approved,
            TagDataVersion = tagDataReview.TagDataVersion,
        };
    }

    public static List<TagDataReviewDto> ToDto(this List<TagDataReview>? tagDataReviews)
    {
        if (tagDataReviews is null) { return new List<TagDataReviewDto>(); }
        return tagDataReviews.Select(ToDto).ToList();
    }

    public static TagDataReview? ToModelOrNull(this TagDataReviewDto? tagDataReviewDto)
    {
        if (tagDataReviewDto is null) { return null; }
        return tagDataReviewDto.ToModel();
    }

    public static TagDataReview ToModel(this TagDataReviewDto tagDataReviewDto)
    {
        return new TagDataReview
        {
            Id = tagDataReviewDto.Id,
            CreatedDate = tagDataReviewDto.CreatedDate,
            ModifiedDate = tagDataReviewDto.ModifiedDate,
            TagNo = tagDataReviewDto.TagNo,
            Status = tagDataReviewDto.Status,
            ApproverId = tagDataReviewDto.ApproverId,
            CommentResponsible = tagDataReviewDto.CommentResponsible,
            Approved = tagDataReviewDto.Approved,
            TagDataVersion = tagDataReviewDto.TagDataVersion,
        };
    }

    public static List<TagDataReview> ToModel(this List<TagDataReviewDto>? tagDataReviewDtos)
    {
        if (tagDataReviewDtos is null) { return new List<TagDataReview>(); }
        return tagDataReviewDtos.Select(ToModel).ToList();
    }
}
