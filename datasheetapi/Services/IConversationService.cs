namespace datasheetapi.Services
{
    public interface IConversationService
    {
        Task<Conversation> CreateConversation(Conversation conversation);
        Task<Conversation> GetConversation(Guid conversationId);
        Task<List<Conversation>> GetConversations(Guid projectId, string tagNo, bool includeLatestMessage);

        Task<Message> AddMessage(Guid conversationId, Message message);
        Task<Message> GetMessage(Guid messageId);
        Task<List<Message>> GetMessages(Guid conversationId);
        Task DeleteMessage(Guid messageId, Guid azureUniqueId);
        Task<Message> UpdateMessage(Guid messageId, Message updatedMessage);
    }
}
