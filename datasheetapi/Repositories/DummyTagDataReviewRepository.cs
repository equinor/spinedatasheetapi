namespace datasheetapi.Repositories;

public class DummyTagDataReviewRepository : ITagDataReviewRepository
{
    private readonly List<TagDataReview> _reviews = new();
    private readonly ILogger<DummyTagDataReviewRepository> _logger;

    public DummyTagDataReviewRepository(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DummyTagDataReviewRepository>();
    }

    public async Task<TagDataReview?> GetTagDataReview(Guid id)
    {
        return await Task.Run(() => _reviews.Find(c => c.Id == id));
    }

    public async Task<List<TagDataReview>> GetTagDataReviews()
    {
        return await Task.Run(() => _reviews);
    }

    public Task<List<TagDataReview>> GetTagDataReviewsForTag(string tagId)
    {
        throw new NotImplementedException();
        // return await Task.Run(() => _reviews.Where(c => c.TagDataId == tagId).ToList());
    }

    public Task<List<TagDataReview>> GetTagDataReviewsForTags(List<string> tagIds)
    {
        throw new NotImplementedException();
        // return await Task.Run(() => _reviews.Where(c => tagIds.Contains(c.TagDataId)).ToList());
    }


    public async Task<TagDataReview> AddTagDataReview(TagDataReview review)
    {
        review.Id = Guid.NewGuid();
        review.CreatedDate = DateTime.UtcNow;
        review.ModifiedDate = DateTime.UtcNow;
        _reviews.Add(review);
        return await Task.Run(() => _reviews.Last());
    }
}
