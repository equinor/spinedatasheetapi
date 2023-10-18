using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class ContainerReviewRepository : IContainerReviewRepository
{
    private readonly ILogger<ContainerReviewRepository> _logger;
    private readonly DatabaseContext _context;

    public ContainerReviewRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<ContainerReviewRepository>();
        _context = context;
    }

    public async Task<ContainerReview?> GetRevisionContainerReview(Guid reviewId)
    {
        var revisionContainerReview = await _context.ContainerReviews.FindAsync(reviewId);
        return revisionContainerReview;
    }

    public async Task<List<ContainerReview>> GetRevisionContainerReviews()
    {
        var revisionContainerReviews = await _context.ContainerReviews.ToListAsync();
        return revisionContainerReviews;
    }

    public async Task<ContainerReview?> GetRevisionContainerReviewForContainer(Guid revisionContainerId)
    {
        var revisionContainerReviews = await _context.ContainerReviews.FirstOrDefaultAsync(c => c.ContainerId == revisionContainerId);
        return revisionContainerReviews;
    }

    public async Task<List<ContainerReview>> GetRevisionContainerReviewsForContainers(List<Guid> tagIds)
    {
        var revisionContainerReviews = await _context.ContainerReviews.Where(c => tagIds.Contains(c.ContainerId)).ToListAsync();
        return revisionContainerReviews;
    }

    public async Task<ContainerReview> AddRevisionContainerReview(ContainerReview review)
    {
        review.Id = Guid.NewGuid();

        var savedReview = _context.ContainerReviews.Add(review);
        await _context.SaveChangesAsync();

        return savedReview.Entity;
    }
}
