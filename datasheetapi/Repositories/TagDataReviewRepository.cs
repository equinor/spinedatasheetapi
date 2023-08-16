using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class TagDataReviewRepository : ITagDataReviewRepository
{
    private readonly ILogger<TagDataReviewRepository> _logger;
    private readonly DatabaseContext _context;

    public TagDataReviewRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<TagDataReviewRepository>();
        _context = context;
    }

    public async Task<TagDataReview?> GetTagDataReview(Guid id)
    {
        var review = await _context.TagDataReviews.FindAsync(id);
        return review;
    }

    public async Task<List<TagDataReview>> GetTagDataReviews()
    {
        var reviews = await _context.TagDataReviews.ToListAsync();
        return reviews;
    }

    public async Task<List<TagDataReview>> GetTagDataReviewsForTag(string tagNo)
    {
        var reviews = await _context.TagDataReviews.Where(c => c.TagNo == tagNo).ToListAsync();
        return reviews;
    }

    public async Task<List<TagDataReview>> GetTagDataReviewsForTags(List<string> tagNos)
    {
        var reviews = await _context.TagDataReviews.Where(c => c.TagNo != null && tagNos.Contains(c.TagNo)).ToListAsync();
        return reviews;
    }


    public async Task<TagDataReview> AddTagDataReview(TagDataReview review)
    {
        review.Id = Guid.NewGuid();
        review.CreatedDate = DateTime.UtcNow;
        review.ModifiedDate = DateTime.UtcNow;

        var savedReview = await _context.TagDataReviews.AddAsync(review);
        await _context.SaveChangesAsync();

        return savedReview.Entity;
    }
}
