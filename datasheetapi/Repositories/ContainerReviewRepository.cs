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

    public async Task<ContainerReview?> GetContainerReview(Guid reviewId)
    {
        var containerReview = await _context.ContainerReviews.FindAsync(reviewId);
        return containerReview;
    }

    public async Task<List<ContainerReview>> GetContainerReviews()
    {
        var containerReviews = await _context.ContainerReviews.ToListAsync();
        return containerReviews;
    }

    public async Task<ContainerReview?> GetContainerReviewForContainer(Guid revisionContainerId)
    {
        var containerReviews =
            await _context.ContainerReviews.FirstOrDefaultAsync(c => c.ContainerId == revisionContainerId);
        return containerReviews;
    }

    public async Task<ContainerReview> AddContainerReview(ContainerReview review)
    {
        var savedReview = _context.ContainerReviews.Add(review);
        await _context.SaveChangesAsync();

        return savedReview.Entity;
    }
}
