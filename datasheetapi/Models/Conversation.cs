namespace datasheetapi.Models;

public class Conversation : BaseEntity
{
    public Guid ProjectId { get; set; }
    public string TagNo { get; set; } = string.Empty;
    public string? Property { get; set; }
    public ConversationLevel ConversationLevel { get; set; }
    public ConversationStatus ConversationStatus { get; set; }
    public List<Participant> Participants { get; set; } = new List<Participant>();
    public List<Message> Messages { get; set; } = new List<Message>();
}

public enum ConversationLevel
{
    Tag,
    Property,
}


public enum ConversationStatus
{
    Open,
    To_be_implemented,
    Closed,
    Implemented
}
