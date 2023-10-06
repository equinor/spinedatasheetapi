using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class ReviewerRepository : IReviewerRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<ReviewerRepository> _logger;

    public ReviewerRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<ReviewerRepository>();
        _context = context;
    }

    public async Task<List<Reviewer>> CreateReviewers(List<Reviewer> reviewers)
    {
        reviewers.ForEach(r =>
        {
            r.CreatedDate = DateTime.UtcNow;
            r.ModifiedDate = DateTime.UtcNow;
        });

        _context.Reviewers.AddRange(reviewers);
        await _context.SaveChangesAsync();

        return reviewers;
    }

    public async Task<Reviewer?> GetReviewer(Guid reviewId, Guid reviewerId)
    {
        var reviewer = await _context.Reviewers.FirstOrDefaultAsync(r => r.TagDataReviewId == reviewId && r.ReviewerId == reviewerId);
        return reviewer;
    }

    public async Task<Reviewer> UpdateReviewer(Reviewer reviewer)
    {
        reviewer.ModifiedDate = DateTime.UtcNow;

        _context.Reviewers.Update(reviewer);

        await _context.SaveChangesAsync();

        return reviewer;
    }
}
