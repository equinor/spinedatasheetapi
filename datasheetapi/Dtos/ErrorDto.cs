namespace datasheetapi.Dtos;

public record ErrorDto
{
    public object Message { get; init; } = string.Empty;
}
