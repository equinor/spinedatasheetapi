using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class ContainerReviewerRepository
{
    private readonly ILogger<ContainerReviewerRepository> _logger;
    private readonly DatabaseContext _context;

    public ContainerReviewerRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<ContainerReviewerRepository>();
        _context = context;
    }

    public async Task<ContainerReviewer?> GetContainerReviewer(Guid reviewId)
    {
        var containerReview = await _context.ContainerReviewers
            .Include(cr => cr.TagReviewers)
            .FirstOrDefaultAsync(cr => cr.Id == reviewId);

        return containerReview;
    }

    public async Task<bool> AnyContainerReviewerWithUserIdAndContainerReviewId(Guid userId, Guid containerReviewId)
    {
        var exists = await _context.ContainerReviewers.AnyAsync(
            cr => cr.ContainerReviewId == containerReviewId
            && cr.UserId == userId);
        return exists;
    }

    public async Task<List<ContainerReviewer>> GetContainerReviewers()
    {
        var containerReviews = await _context.ContainerReviewers.ToListAsync();
        return containerReviews;
    }

    public async Task<List<ContainerReviewer>> GetContainerReviewersForContainerReview(Guid containerReviewId, Guid userId)
    {
        var collection = _context.ContainerReviewers as IQueryable<ContainerReviewer>;

        collection = collection.Where(cr => cr.ContainerReviewId == containerReviewId);

        if (userId !=  Guid.Empty)
        {
            collection = collection.Where(cr => cr.UserId == userId);
        }

        var containerReviewers = await collection.ToListAsync();
        return containerReviewers;
    }

    public async Task<ContainerReviewer> CreateContainerReviewer(ContainerReviewer review)
    {
        var savedReview = _context.ContainerReviewers.Add(review);
        await _context.SaveChangesAsync();

        return savedReview.Entity;
    }
}
