namespace datasheetapi.Dtos;
public record UserDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
}
