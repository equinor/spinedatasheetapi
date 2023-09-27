using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class RevisionContainerReviewRepository : IRevisionContainerReviewRepository
{
    private readonly ILogger<RevisionContainerReviewRepository> _logger;
    private readonly DatabaseContext _context;

    public RevisionContainerReviewRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<RevisionContainerReviewRepository>();
        _context = context;
    }

    public async Task<RevisionContainerReview?> GetRevisionContainerReview(Guid reviewId)
    {
        var revisionContainerReview = await _context.RevisionContainerReviews.FindAsync(reviewId);
        return revisionContainerReview;
    }

    public async Task<List<RevisionContainerReview>> GetRevisionContainerReviews()
    {
        var revisionContainerReviews = await _context.RevisionContainerReviews.ToListAsync();
        return revisionContainerReviews;
    }

    public async Task<RevisionContainerReview?> GetRevisionContainerReviewForContainer(Guid revisionContainerId)
    {
        var revisionContainerReviews = await _context.RevisionContainerReviews.FirstOrDefaultAsync(c => c.RevisionContainerId == revisionContainerId);
        return revisionContainerReviews;
    }

    public async Task<List<RevisionContainerReview>> GetRevisionContainerReviewsForContainers(List<Guid> tagIds)
    {
        var revisionContainerReviews = await _context.RevisionContainerReviews.Where(c => tagIds.Contains(c.RevisionContainerId)).ToListAsync();
        return revisionContainerReviews;
    }

    public async Task<RevisionContainerReview> AddRevisionContainerReview(RevisionContainerReview review)
    {
        review.Id = Guid.NewGuid();
        review.CreatedDate = DateTime.UtcNow;
        review.ModifiedDate = DateTime.UtcNow;

        var savedReview = _context.RevisionContainerReviews.Add(review);
        await _context.SaveChangesAsync();

        return savedReview.Entity;
    }
}
