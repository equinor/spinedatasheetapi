namespace datasheetapi.Repositories;

public interface ITagDataReviewRepository
{
    Task<TagDataReview?> GetTagDataReview(Guid id);
    Task<List<TagDataReview>> GetTagDataReviews();
    Task<List<TagDataReview>> GetTagDataReviewsForTag(Guid tagId);
    Task<List<TagDataReview>> GetTagDataReviewsForTags(List<Guid> tagIds);
    Task<TagDataReview> AddTagDataReview(TagDataReview review);
}
