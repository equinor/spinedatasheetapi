namespace datasheetapi.Dtos
{
    public interface ITagDataDto
    {
        Guid Id { get; set; }
        Guid ProjectId { get; set; }
        string? TagNo { get; set; }
        string? Description { get; set; }
        string? Category { get; set; }
        string? Area { get; set; }
        string? Discipline { get; set; }
        int Version { get; set; }
        Review? Review { get; set; }
        RevisionPackage? RevisionPackage { get; set; }
    }
}
