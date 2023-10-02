namespace datasheetapi.Repositories;

public interface ITagDataReviewRepository
{
    Task<TagDataReview?> GetTagDataReview(Guid id);
    Task<bool> AnyTagDataReview(Guid id);
    Task<List<TagDataReview>> GetTagDataReviews();
    Task<List<TagDataReview>> GetTagDataReviewsForTag(string tagNo);
    Task<List<TagDataReview>> GetTagDataReviewsForTags(List<string> tagNos);
    Task<TagDataReview> AddTagDataReview(TagDataReview review);
}
