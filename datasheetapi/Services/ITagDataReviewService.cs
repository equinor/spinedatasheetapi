namespace datasheetapi.Services;

public interface ITagDataReviewService
{
    Task<TagDataReview?> GetTagDataReview(Guid id);
    Task<List<TagDataReview>> GetTagDataReviews();
    Task<List<TagDataReview>> GetTagDataReviewsForProject(Guid projectId);
    Task<List<TagDataReview>> GetTagDataReviewsForTag(Guid tagId);
    Task<List<TagDataReview>> GetTagDataReviewsForTags(List<Guid> tagIds);
    Task<TagDataReview> CreateTagDataReview(TagDataReview review, Guid azureUniqueId);
}
