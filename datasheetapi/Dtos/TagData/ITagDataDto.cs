namespace datasheetapi.Dtos
{
    public interface ITagDataDto
    {
        Guid Id { get; set; }
        string? TagNo { get; set; }
        string? Description { get; set; }
        string? Category { get; set; }
        string? Area { get; set; }
        string? Discipline { get; set; }
        int Version { get; set; }
        TagDataReviewDto? Review { get; set; }
        RevisionContainer? RevisionContainer { get; set; }
    }
}
