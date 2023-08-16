namespace datasheetapi.Services;

public interface ITagDataReviewService
{
    Task<TagDataReview?> GetTagDataReview(Guid id);
    Task<List<TagDataReview>> GetTagDataReviews();
    Task<List<TagDataReview>> GetTagDataReviewsForProject(Guid projectId);
    Task<List<TagDataReview>> GetTagDataReviewsForTag(string tagNo);
    Task<List<TagDataReview>> GetTagDataReviewsForTags(List<string> tagNos);
    Task<TagDataReview> CreateTagDataReview(TagDataReview review, Guid azureUniqueId);
}
