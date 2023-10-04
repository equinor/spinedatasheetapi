using api.Database;

namespace datasheetapi.Repositories;

public class ReviewerTagDataReviewRepository : IReviewerTagDataReviewRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<ReviewerTagDataReviewRepository> _logger;

    public ReviewerTagDataReviewRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<ReviewerTagDataReviewRepository>();
        _context = context;
    }

    public async Task<ReviewerTagDataReview> CreateReviewerTagDataReview(ReviewerTagDataReview review)
    {
        var savedReview = _context.ReviewerTagDataReviews.Add(review);
        await _context.SaveChangesAsync();
        return savedReview.Entity;
    }
}
