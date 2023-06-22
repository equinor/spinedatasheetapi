namespace datasheetapi.Models;

public class TagData : ITagData
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string? TagNo { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Area { get; set; }
    public string? Discipline { get; set; }
    public List<Revision> Revisions { get; set; } = new List<Revision>();
}
