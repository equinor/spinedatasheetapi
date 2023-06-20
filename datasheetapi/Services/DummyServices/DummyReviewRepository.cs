namespace datasheetapi.Services;

public class DummyReviewRepository
{
    private List<Review> _reviews = new();
    private readonly ILogger<DummyReviewRepository> _logger;

    public DummyReviewRepository(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DummyReviewRepository>();
    }

    public async Task<Review?> GetReview(Guid id)
    {
        return await Task.Run(() => _reviews.Find(c => c.Id == id));
    }

    public async Task<List<Review>> GetReviews()
    {
        return await Task.Run(() => _reviews);
    }

    public async Task<List<Review>> GetReviewsForTag(Guid tagId)
    {
        return await Task.Run(() => _reviews.Where(c => c.TagId == tagId).ToList());
    }

    public async Task<List<Review>> GetReviewsForTags(List<Guid> tagIds)
    {
        return await Task.Run(() => _reviews.Where(c => tagIds.Contains(c.TagId)).ToList());
    }


    public async Task<Review> AddComment(Review review)
    {
        review.Id = Guid.NewGuid();
        review.CreatedDate = DateTime.UtcNow;
        review.ModifiedDate = DateTime.UtcNow;
        _reviews.Add(review);
        return await Task.Run(() => _reviews.Last());
    }
}
