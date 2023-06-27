namespace datasheetapi.Dtos;

public class TagDataDto : ITagDataDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string? TagNo { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Area { get; set; }
    public string? Discipline { get; set; }
    public int RevisionNumber { get; set; }
    public Review? Review { get; set; }
    public Package? Package { get; set; }
}
