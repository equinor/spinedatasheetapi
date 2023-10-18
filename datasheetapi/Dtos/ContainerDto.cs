using System.Text.Json.Serialization;

namespace datasheetapi.Dtos;
public record ContainerDto : BaseEntityDto
{
    public string RevisionContainerName { get; set; } = string.Empty;
    public int RevisionNumber { get; set; }
    public DateTimeOffset RevisionContainerDate { get; set; } = DateTimeOffset.Now;

    [JsonConverter(typeof(ListITagDataDtoConverter))]
    public List<ITagDataDto> TagData { get; set; } = new List<ITagDataDto>();
    public ContainerReviewDto? RevisionContainerReview { get; set; }
    public Guid ContractId { get; set; }
    public ContractDto? Contract { get; set; }
}
