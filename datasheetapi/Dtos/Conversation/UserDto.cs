namespace datasheetapi.Dtos;
public record UserDto
{
    public Guid UserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
}
