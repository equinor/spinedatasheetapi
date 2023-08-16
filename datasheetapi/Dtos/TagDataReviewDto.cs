namespace datasheetapi.Dtos;

public record TagDataReviewDto : BaseEntityDto
{
    public string? TagNo { get; set; }
    public ReviewStatusEnum Status { get; set; }
    public Guid ApproverId { get; set; }
    public Guid CommentResponsible { get; set; }
    public bool Approved { get; set; }
    public int TagDataVersion { get; init; }
    public List<CommentDto>? Comments { get; set; } = new List<CommentDto>();
}
