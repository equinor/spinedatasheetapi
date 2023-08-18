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

    public async Task<Comment?> GetComment(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);
        return comment;
    }

    public async Task<List<Comment>> GetComments()
    {
        var comments = await _context.Comments.ToListAsync();
        return comments;
    }

    public async Task DeleteComment(Comment comment)
    {
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<Comment> UpdateComment(Comment entity)
    {
        entity.ModifiedDate = DateTime.UtcNow;

        var updatedComment = _context.Comments.Update(entity);
        await _context.SaveChangesAsync();

        return updatedComment.Entity;
    }

    public async Task<List<Comment>> GetCommentsForTagReview(Guid tagId)
    {
        var comments = await _context.Comments.Where(c => c.TagDataReviewId == tagId).ToListAsync();
        return comments;
    }

    public async Task<List<Comment>> GetCommentsForRevisionContainerReview(Guid tagId)
    {
        var comments = await _context.Comments.Where(c => c.RevisionContainerReviewId == tagId).ToListAsync();
        return comments;
    }

    public async Task<List<Comment>> GetCommentsForTagReviews(List<Guid?> tagIds)
    {
        var comments = await _context.Comments.Where(c => tagIds.Contains(c.TagDataReviewId)).ToListAsync();
        return comments;
    }

    public async Task<List<Comment>> GetCommentsForRevisionContainerReviews(List<Guid?> tagIds)
    {
        var comments = await _context.Comments.Where(c => tagIds.Contains(c.RevisionContainerReviewId)).ToListAsync();
        return comments;
    }


    public async Task<Comment> AddComment(Comment comment)
    {
        comment.Id = Guid.NewGuid();
        comment.CreatedDate = DateTime.UtcNow;
        comment.ModifiedDate = DateTime.UtcNow;

        var savedComment = _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return savedComment.Entity;
    }
}
