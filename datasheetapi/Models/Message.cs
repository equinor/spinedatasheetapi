namespace datasheetapi.Models;

public class Message : BaseEntity
{
    public Guid UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public Guid ConversationId { get; set; }
    public Conversation? Conversation { get; set; }
    public bool IsEdited { get; set; }
    public bool SoftDeleted { get; set; }

    public void SetConversation(Conversation conversation)
    {
        Conversation = conversation;
        ConversationId = conversation.Id;
    }
}
