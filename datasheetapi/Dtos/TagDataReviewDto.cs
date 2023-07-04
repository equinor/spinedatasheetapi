namespace datasheetapi.Dtos;

public record TagDataReviewDto
{
    public Guid TagId { get; set; }
    public Guid RevisionId { get; set; }
    public ReviewStatusEnum Status { get; set; }
    public Guid ApproverId { get; set; }
    public Guid CommentResponsible { get; set; }
    public bool Approved { get; set; }
    public int TagDataVersion { get; init; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public static TagDataReviewDto? MapTagDataReviewToTagDataReviewDto(TagDataReview? tagDataReview)
    {
        if (tagDataReview == null) { return null; }
        return new TagDataReviewDto
        {
            TagId = tagDataReview.TagDataId,
            Status = tagDataReview.Status,
            ApproverId = tagDataReview.ApproverId,
            CommentResponsible = tagDataReview.CommentResponsible,
            Approved = tagDataReview.Approved,
            TagDataVersion = tagDataReview.TagDataVersion,
            Comments = tagDataReview.Comments,
        };
    }
}