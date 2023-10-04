using api.Database;

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
        var savedReviewers = new List<Reviewer>();
        reviewers.ForEach(r =>
        {
            r.CreatedDate = DateTime.UtcNow;
            r.ModifiedDate = DateTime.UtcNow;
        });

        _context.Reviewers.AddRange(reviewers);
        await _context.SaveChangesAsync();

        return reviewers;
    }
}
