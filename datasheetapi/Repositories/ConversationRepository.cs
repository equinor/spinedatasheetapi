using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class ConversationRepository : IConversationRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<ConversationRepository> _logger;

    public ConversationRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<ConversationRepository>();
        _context = context;
    }

    public async Task<Message?> GetMessage(Guid messageId)
    {
        var message = await _context.Messages.FindAsync(messageId);
        return message;
    }

    public async Task<List<Message>> GetMessages(Guid conversationId)
    {
        var messages = await _context.Messages
            .Where(c => c.ConversationId == conversationId).ToListAsync();
        return messages;
    }

    public async Task DeleteMessage(Message message)
    {
        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();
    }


    public async Task<Message> UpdateMessage(Message entity)
    {
        entity.ModifiedDate = DateTime.UtcNow;

        var updatedMessage = _context.Messages.Update(entity);
        await _context.SaveChangesAsync();

        return updatedMessage.Entity;
    }


    public async Task<Message> AddMessage(Message message)
    {
        message.Id = Guid.NewGuid();
        message.CreatedDate = DateTime.UtcNow;
        message.ModifiedDate = DateTime.UtcNow;
        if (GetParticipant(message.UserId, message.ConversationId) == null)
        {
            _context.Participants
                .Add(new()
                {
                    UserId = message.UserId,
                    ConversationId = message.ConversationId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                });
        }

        var savedMessage = _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return savedMessage.Entity;
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

        var savedConversation = _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync();
        return savedConversation.Entity;
    }

    public async Task<Conversation?> GetConversation(Guid conversationId)
    {
        return await _context.Conversations
            .Include(p => p.Participants)
            .Include(p => p.Messages)
            .Where(p => p.Id == conversationId).FirstOrDefaultAsync();
    }

    public async Task<List<Conversation>> GetConversations(Guid projectId, string tagNo)
    {
        return await _context.Conversations
            .Include(c => c.Participants)
            .Where(c => c.ProjectId == projectId && c.TagNo == tagNo).ToListAsync();
    }

    public async Task<List<Conversation>> GetConversationsForTagNos(ICollection<string> tagNos)
    {
        var conversationWithLatestMessage = await _context.Conversations
            .Include(c => c.Participants)
            .Where(c => tagNos.Contains(c.TagNo))
            .Select(c => new
            {
                Conversation = c,
                LatestMessage = c.Messages
                                    .OrderByDescending(m => m.CreatedDate)
                                    .FirstOrDefault(m => m.SoftDeleted == false)
                                ?? c.Messages
                                    .OrderByDescending(m => m.CreatedDate)
                                    .First()
            })
            .ToListAsync();

        return conversationWithLatestMessage
            .Select(c =>
            {
                c.Conversation.Messages = new List<Message> { c.LatestMessage };
                return c.Conversation;
            })
            .ToList();
    }

    public async Task<List<Conversation>> GetConversationsWithLatestMessage(Guid projectId,
        string tagNo, bool includeSoftDeletedMessage)
    {
        var conversationWithLatestMessage = await _context.Conversations
            .Include(c => c.Participants)
            .Where(c => c.ProjectId == projectId && c.TagNo == tagNo)
            .Select(c => new
            {
                Conversation = c,
                LatestMessage = c.Messages
                                    .OrderByDescending(m => m.CreatedDate)
                                    .FirstOrDefault(m => m.SoftDeleted == includeSoftDeletedMessage)
                                ?? c.Messages
                                    .OrderByDescending(m => m.CreatedDate)
                                    .First()
            })
            .ToListAsync();

        return conversationWithLatestMessage
            .Select(c =>
            {
                c.Conversation.Messages =
                    new List<Message> { c.LatestMessage };
                return c.Conversation;
            })
            .ToList();
    }
}
