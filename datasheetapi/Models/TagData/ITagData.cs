namespace datasheetapi.Models
{
    public interface ITagData
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
        int Version { get; set; }
    }
}
