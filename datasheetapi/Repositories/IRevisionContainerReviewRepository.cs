namespace datasheetapi.Repositories;

public interface IRevisionContainerReviewRepository
{
    Task<RevisionContainerReview?> GetRevisionContainerReview(Guid id);
    Task<List<RevisionContainerReview>> GetRevisionContainerReviews();
    Task<RevisionContainerReview?> GetRevisionContainerReviewForRevision(Guid tagId);
    Task<List<RevisionContainerReview>> GetRevisionContainerReviewsForRevisions(List<Guid> tagIds);
    Task<RevisionContainerReview> AddRevisionContainerReview(RevisionContainerReview review);
}
