namespace datasheetapi.Services;

public interface ITagDataReviewService
{
    Task<TagDataReview> GetTagDataReview(Guid id);
    Task<bool> AnyTagDataReview(Guid reviewId);
    Task<List<TagDataReview>> GetTagDataReviews();
    Task<List<TagDataReview>> GetTagDataReviewsForTag(string tagNo);
    Task<List<TagDataReview>> GetTagDataReviewsForTags(List<string> tagNos);
    Task<TagDataReview> CreateTagDataReview(TagDataReview review, Guid azureUniqueId);
}
