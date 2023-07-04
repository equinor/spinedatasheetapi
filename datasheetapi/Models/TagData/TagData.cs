namespace datasheetapi.Models;

public class TagData : BaseEntity, ITagData
{
    public TagData()
    {
    }

    // public Guid ProjectId { get; set; }
    public string? TagNo { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Area { get; set; }
    public string? Discipline { get; set; }
    public int Version { get; set; }

    // Relationships
    public RevisionContainer? RevisionContainer { get; set; }
    public Guid TagDataReviewId { get; set; }
    public TagDataReview? TagDataReview { get; set; }
}
