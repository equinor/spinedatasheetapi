namespace datasheetapi.Services
{
    public interface ICommentService
    {
        Task<Conversation> CreateConversation(Conversation conversation);
        Task<Conversation?> GetConversation(Guid conversationId);
        Task<List<Conversation>> GetConversations(Guid reviewId);

        Task<Message> AddComment(Guid conversationId, Message comment);
        Task<Message?> GetComment(Guid commentId);
        Task<List<Message>?> GetComments(Guid conversationId);
        Task DeleteComment(Guid commentId, Guid azureUniqueId);
        Task<Message> UpdateComment(Guid commentId, Message updatedComment);
        Task<Dictionary<Guid, string>> GetUserIdUserName(List<Guid> userIds);
        Task<string> GetUserName(Guid userId);

    }
}
