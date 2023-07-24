namespace datasheetapi.Dtos;
public record class ContractDto : BaseEntityDto
{
    public string ContractName { get; set; } = string.Empty;
    public Guid ContractorId { get; set; }

    // Relationships
    public Guid ProjectId { get; set; }
    public List<RevisionContainerDto> RevisionContainers { get; set; } = new List<RevisionContainerDto>();
}
