namespace datasheetapi.Dtos;

public record UserTagDto
{
    public string AzureUniqueId { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string Mail { get; init; } = string.Empty;
    public string AccountType { get; init; } = string.Empty;
}
