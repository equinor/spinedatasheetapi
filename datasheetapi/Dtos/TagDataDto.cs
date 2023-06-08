namespace datasheetapi.Dtos;

public class TagDataDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string? TagNo { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Area { get; set; }
    public string? Dicipline { get; set; }
}
