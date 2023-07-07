namespace datasheetapi.Adapters;
public static class RevisionContainerReviewAdapter
{
    public static RevisionContainerReviewDto? ToDtoOrNull(this RevisionContainerReview? revisionContainerReview)
    {
        if (revisionContainerReview is null) { return null; }
        return revisionContainerReview.ToDto();
    }

    private static RevisionContainerReviewDto ToDto(this RevisionContainerReview revisionContainerReview)
    {
        return new RevisionContainerReviewDto
        {
            Id = revisionContainerReview.Id,
            CreatedDate = revisionContainerReview.CreatedDate,
            ModifiedDate = revisionContainerReview.ModifiedDate,
            Status = revisionContainerReview.Status,
            ApproverId = revisionContainerReview.ApproverId,
            CommentResponsible = revisionContainerReview.CommentResponsible,
            Approved = revisionContainerReview.Approved,
            RevisionContainerVersion = revisionContainerReview.RevisionContainerVersion,
            Comments = revisionContainerReview.Comments.ToDto(),
        };
    }

    public static List<RevisionContainerReviewDto> ToDto(this List<RevisionContainerReview>? revisionContainerReviews)
    {
        if (revisionContainerReviews is null) { return new List<RevisionContainerReviewDto>(); }
        return revisionContainerReviews.Select(ToDto).ToList();
    }

    public static RevisionContainerReview? ToModelOrNull(this RevisionContainerReviewDto? revisionContainerReviewDto)
    {
        if (revisionContainerReviewDto is null) { return null; }
        return revisionContainerReviewDto.ToModel();
    }

    private static RevisionContainerReview ToModel(this RevisionContainerReviewDto revisionContainerReviewDto)
    {
        return new RevisionContainerReview
        {
            Id = revisionContainerReviewDto.Id,
            CreatedDate = revisionContainerReviewDto.CreatedDate,
            ModifiedDate = revisionContainerReviewDto.ModifiedDate,
            Status = revisionContainerReviewDto.Status,
            ApproverId = revisionContainerReviewDto.ApproverId,
            CommentResponsible = revisionContainerReviewDto.CommentResponsible,
            Approved = revisionContainerReviewDto.Approved,
            RevisionContainerVersion = revisionContainerReviewDto.RevisionContainerVersion,
            Comments = revisionContainerReviewDto.Comments.ToModel(),
        };
    }

    public static List<RevisionContainerReview> ToModel(this List<RevisionContainerReviewDto>? revisionContainerReviewDtos)
    {
        if (revisionContainerReviewDtos is null) { return new List<RevisionContainerReview>(); }
        return revisionContainerReviewDtos.Select(ToModel).ToList();
    }
}
