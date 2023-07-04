namespace datasheetapi.Repositories;

public class DummyRevisionContainerReviewRepository : IRevisionContainerReviewRepository
{
    private readonly List<RevisionContainerReview> _reviews = new();
    private readonly ILogger<DummyTagDataReviewRepository> _logger;

    public DummyRevisionContainerReviewRepository(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DummyTagDataReviewRepository>();
    }

    public async Task<RevisionContainerReview?> GetTagDataReview(Guid id)
    {
        return await Task.Run(() => _reviews.Find(c => c.Id == id));
    }

    public async Task<List<RevisionContainerReview>> GetTagDataReviews()
    {
        return await Task.Run(() => _reviews);
    }

    public async Task<List<RevisionContainerReview>> GetTagDataReviewsForTag(Guid tagId)
    {
        return await Task.Run(() => _reviews.Where(c => c.TagId == tagId).ToList());
    }

    public async Task<List<RevisionContainerReview>> GetTagDataReviewsForTags(List<Guid> tagIds)
    {
        return await Task.Run(() => _reviews.Where(c => tagIds.Contains(c.TagId)).ToList());
    }


    public async Task<RevisionContainerReview> AddTagDataReview(RevisionContainerReview review)
    {
        review.Id = Guid.NewGuid();
        review.CreatedDate = DateTime.UtcNow;
        review.ModifiedDate = DateTime.UtcNow;
        _reviews.Add(review);
        return await Task.Run(() => _reviews.Last());
    }
}
