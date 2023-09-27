namespace datasheetapi.Repositories;

public interface IRevisionContainerReviewRepository
{
    Task<RevisionContainerReview?> GetRevisionContainerReview(Guid reviewId);
    Task<List<RevisionContainerReview>> GetRevisionContainerReviews();
    Task<RevisionContainerReview?> GetRevisionContainerReviewForContainer(Guid revisionContainerId);
    Task<List<RevisionContainerReview>> GetRevisionContainerReviewsForContainers(List<Guid> revisionContainerIds);
    Task<RevisionContainerReview> AddRevisionContainerReview(RevisionContainerReview review);
}
