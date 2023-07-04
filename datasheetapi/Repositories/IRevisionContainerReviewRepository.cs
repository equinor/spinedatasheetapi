namespace datasheetapi.Repositories;

public interface IRevisionContainerReviewRepository
{
    Task<RevisionContainerReview?> GetTagDataReview(Guid id);
    Task<List<RevisionContainerReview>> GetTagDataReviews();
    Task<List<RevisionContainerReview>> GetTagDataReviewsForTag(Guid tagId);
    Task<List<RevisionContainerReview>> GetTagDataReviewsForTags(List<Guid> tagIds);
    Task<RevisionContainerReview> AddTagDataReview(RevisionContainerReview review);
}