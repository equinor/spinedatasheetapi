namespace datasheetapi.Models
{
    public interface ITagData
    {
        Guid Id { get; set; }
        Guid ProjectId { get; set; }
        string? TagNo { get; set; }
        string? Description { get; set; }
        string? Category { get; set; }
        string? Area { get; set; }
        string? Discipline { get; set; }
        int Version { get; set; }
        TagDataReview? Review { get; set; }
        RevisionPackage? RevisionPackage { get; set; }
    }
}
