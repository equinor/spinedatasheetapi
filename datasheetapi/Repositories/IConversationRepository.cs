namespace datasheetapi.Repositories;

public interface IConversationRepository
{
    Task<Conversation> CreateConversation(Conversation conversation);
    Task<Conversation?> GetConversation(Guid conversationId);
    Task<List<Conversation>> GetConversationsWithLatestMessage(Guid reviewId, bool includeSoftDeletedMessage);
    Task<List<Conversation>> GetConversations(Guid reviewId);

    Task<Message> AddMessage(Message message);
    Task<Message?> GetMessage(Guid messageId);
    Task<List<Message>> GetMessages(Guid conversationId);
    Task DeleteMessage(Message message);
    Task<Message> UpdateMessage(Message oldMessage);
}
