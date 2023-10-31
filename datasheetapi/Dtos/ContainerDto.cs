using System.Text.Json.Serialization;

namespace datasheetapi.Dtos;
public record ContainerDto : BaseEntityDto
{
    public string ContainerName { get; set; } = string.Empty;
    public int RevisionNumber { get; set; }
    public DateTimeOffset ContainerDate { get; set; } = DateTimeOffset.Now;

    public List<string> TagNos { get; set; } = new List<string>();
    public Guid ContractId { get; set; }
}
