namespace datasheetapi.Dtos;
public record RevisionContainerDto : BaseEntityDto
{
    public string RevisionContainerName { get; set; } = string.Empty;
    public int RevisionNumber { get; set; }
    public DateTimeOffset RevisionContainerDate { get; set; } = DateTimeOffset.Now;

    // Relationships
    public List<ITagDataDto> TagData { get; set; } = new List<ITagDataDto>();
    public RevisionContainerReviewDto? RevisionContainerReview { get; set; }
    public Guid ContractId { get; set; }
    public ContractDto? Contract { get; set; }
}
