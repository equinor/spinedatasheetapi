using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<CommentRepository> _logger;

    public CommentRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<CommentRepository>();
        _context = context;
    }

    public async Task<Message?> GetComment(Guid id)
    {
        var comment = await _context.Messages.FindAsync(id);
        return comment;
    }

    public async Task<List<Message>> GetComments(Guid conversationId)
    {
        var comments = await _context.Messages.Where(c => c.ConversationId == conversationId).ToListAsync();
        return comments;
    }

    public async Task DeleteComment(Message comment)
    {
        _context.Messages.Remove(comment);
        await _context.SaveChangesAsync();
    }


    public async Task<Message> UpdateComment(Message entity)
    {
        entity.ModifiedDate = DateTime.UtcNow;

        var updatedComment = _context.Messages.Update(entity);
        await _context.SaveChangesAsync();

        return updatedComment.Entity;
    }


    public async Task<Message> AddComment(Message comment)
    {
        comment.Id = Guid.NewGuid();
        comment.CreatedDate = DateTime.UtcNow;
        comment.ModifiedDate = DateTime.UtcNow;
        if (GetParticipant(comment.UserId, comment.ConversationId) == null)
        {
            _context.Participants
                .Add(new Participant(comment.UserId, comment.ConversationId));
        }
        var savedComment = _context.Messages.Add(comment);
        await _context.SaveChangesAsync();
        return savedComment.Entity;
    }

    private Participant? GetParticipant(Guid userId, Guid conversationId)
    {
        return _context.Participants
            .Where(p => p.ConversationId == conversationId && p.UserId == userId)
            .FirstOrDefault();
    }

    public async Task<Conversation> CreateConversation(Conversation conversation)
    {
        conversation.Id = Guid.NewGuid();
        conversation.CreatedDate = DateTime.UtcNow;
        conversation.ModifiedDate = DateTime.UtcNow;

        var savedComment = _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync();
        return savedComment.Entity;
    }

    public async Task<Conversation?> GetConversation(Guid conversationId)
    {
        return await _context.Conversations
                .Include(p => p.Participants)
                .Include(p => p.Messages)
                .Where(p => p.Id == conversationId).FirstOrDefaultAsync();
    }

    public async Task<List<Conversation>> GetConversations(Guid reviewId)
    {
        return await _context.Conversations
                .Include(p => p.Participants)
                .Where(c => c.TagDataReviewId == reviewId).ToListAsync();
    }
}
