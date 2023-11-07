namespace datasheetapi.Dtos.Conversation;
public class GetConversationDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string TagNo { get; set; } = string.Empty;
    public string? Property { get; set; }
    public ConversationStatusDto ConversationStatus { get; set; }
    public ConversationLevelDto ConversationLevel { get; set; }
    public List<UserDto> Participants { get; set; } = new List<UserDto>();
    public List<GetMessageDto>? Messages { get; set; }
}
