namespace datasheetapi.Repositories;

public interface ICommentRepository
{
    Task<Conversation> CreateConversation(Conversation comment);
    Task<Conversation?> GetConversation(Guid conversationId);
    Task<List<Conversation>> GetConversations(Guid reviewId);

    Task<Message> AddComment(Message comment);
    Task<Message?> GetComment(Guid commentId);
    Task<List<Message>> GetComments(Guid conversationId);
    Task DeleteComment(Message comment);
    Task<Message> UpdateComment(Message oldComment);
}
