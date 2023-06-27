namespace datasheetapi.Models;

public class TagData : BaseEntity, ITagData
{
    public TagData(Package package)
    {
        Package = package;
    }

    public Guid ProjectId { get; set; }
    public string? TagNo { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Area { get; set; }
    public string? Discipline { get; set; }
    public int RevisionNumber { get; set; }
    public Package Package { get; set; }
    public Review? Review { get; set; }
}
