namespace datasheetapi.Repositories;

public interface IRevisionContainerReviewRepository
{
    Task<RevisionContainerReview?> GetTagDataReview(Guid id);
    Task<List<RevisionContainerReview>> GetTagDataReviews();
    Task<List<RevisionContainerReview>> GetRevisionContainerReviewForRevision(Guid tagId);
    Task<List<RevisionContainerReview>> GetRevisionContainerReviewsForRevisions(List<Guid> tagIds);
    Task<RevisionContainerReview> AddTagDataReview(RevisionContainerReview review);
}