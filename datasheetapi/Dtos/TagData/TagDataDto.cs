namespace datasheetapi.Dtos;

public record TagDataDto : ITagDataDto, IBaseEntityDto
{
    public Guid Id { get; set; }
    public string? TagNo { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Area { get; set; }
    public string? Discipline { get; set; }
    public int Version { get; set; }
    public TagDataReviewDto? Review { get; set; }
    public RevisionContainerDto? RevisionContainer { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}
