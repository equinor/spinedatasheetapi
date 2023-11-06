namespace datasheetapi.Repositories;

public interface IConversationRepository
{
    Task<Conversation> CreateConversation(Conversation conversation);
    Task<Conversation?> GetConversation(Guid conversationId);
    Task<List<Conversation>> GetConversationsForTagNos(ICollection<string> tagNos);
    Task<List<Conversation>> GetConversationsWithLatestMessage(Guid projectId,
        string tagNo, bool includeSoftDeletedMessage);
    Task<List<Conversation>> GetConversations(Guid projectId, string tagNo);

    Task<Message> AddMessage(Message message);
    Task<Message?> GetMessage(Guid messageId);
    Task<List<Message>> GetMessages(Guid conversationId);
    Task DeleteMessage(Message message);
    Task<Message> UpdateMessage(Message oldMessage);
}
