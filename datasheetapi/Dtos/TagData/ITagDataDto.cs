using System.Text.Json.Serialization;

namespace datasheetapi.Dtos
{
    [JsonConverter(typeof(ITagDataDtoConverter))]
    public interface ITagDataDto
    {
        Guid Id { get; set; }
        string? TagNo { get; set; }
        string? Description { get; set; }
        string? Category { get; set; }
        string? Area { get; set; }
        string? Discipline { get; set; }
        string? Contract { get; set; }
        string? ContractName { get; set; }
        string? TagStatus { get; set; }
        string? EngineeringCode { get; set; }
        string? PurchaseOrder { get; set; }
        string? Sequence { get; set; }
        string? System { get; set; }
        string? TagType { get; set; }
        int Version { get; set; }
        TagDataReviewDto? Review { get; set; }
        RevisionContainerDto? RevisionContainer { get; set; }
    }
}
