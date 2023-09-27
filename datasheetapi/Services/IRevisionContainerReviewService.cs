namespace datasheetapi.Services;

public interface IRevisionContainerReviewService
{
    Task<RevisionContainerReview> GetRevisionContainerReview(Guid id);
    Task<List<RevisionContainerReview>> GetRevisionContainerReviews();
    Task<RevisionContainerReview?> GetRevisionContainerReviewForContainer(Guid revisionContainerId);
    Task<RevisionContainerReview> CreateRevisionContainerReview(RevisionContainerReview review, Guid azureUniqueId);
}
