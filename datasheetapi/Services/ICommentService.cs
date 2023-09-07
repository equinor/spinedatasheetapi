namespace datasheetapi.Services
{
    public interface ICommentService
    {
        // Task<CommentDto?> GetCommentDto(Guid id);
        Task<Conversation> CreateConversation(Conversation conversation);
        Task<Conversation?> GetConversation(Guid conversationId);
        Task<List<Conversation>> GetConversations(Guid reviewId);

        Task<Message> AddComment(Message comment);
        Task<GetMessageDto?> GetComment(Guid id);
        Task<List<GetMessageDto>?> GetComments(Guid conversationId);
        Task DeleteComment(Guid commentId, Guid azureUniqueId);
        Task<CommentDto?> UpdateComment(Guid azureUniqueId, Message updatedComment);

    }
}
