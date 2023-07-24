namespace datasheetapi.Dtos;
public record class ProjectDto : BaseEntityDto
{
    public List<ContractDto> Contracts { get; set; } = new List<ContractDto>();
}
