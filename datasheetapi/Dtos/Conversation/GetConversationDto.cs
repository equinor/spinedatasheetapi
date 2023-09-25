namespace datasheetapi.Dtos;
public record GetConversationDto : BaseEntityDto
{
    public string? Property { get; set; }
    public ConversationStatusDto ConversationStatus { get; set; }
    public ConversationLevelDto ConversationLevel { get; set; }
    public List<UserDto> Participants { get; set; } = new List<UserDto>();
    public List<GetMessageDto>? Messages { get; set; }
}
