namespace datasheetapi.Services
{
    public interface IConversationService
    {
        Task<Conversation> CreateConversation(Conversation conversation);
        Task<Conversation> GetConversation(Guid conversationId);
        Task<List<Conversation>> GetConversations(Guid reviewId);

        Task<Message> AddMessage(Guid conversationId, Message message);
        Task<Message> GetMessage(Guid messageId);
        Task<List<Message>> GetMessages(Guid conversationId);
        Task DeleteMessage(Guid messageId, Guid azureUniqueId);
        Task<Message> UpdateMessage(Guid messageId, Message updatedMessage);
        Task<Dictionary<Guid, string>> GetUserIdUserName(List<Guid> userIds);
        Task<string> GetUserName(Guid userId);

    }
}
