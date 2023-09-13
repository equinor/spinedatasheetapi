namespace datasheetapi.Dtos;
public record GetConversationDto : BaseEntityDto
{
    public string? Property { get; set; }
    public ConversationStatus ConversationStatus { get; set; }
    public ConversationLevel ConversationLevel { get; set; }
    public List<UserDto> Participants { get; set; } = new List<UserDto>();
    public List<GetMessageDto>? Messages { get; set; }
}
