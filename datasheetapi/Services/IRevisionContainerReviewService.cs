namespace datasheetapi.Services;

public interface IRevisionContainerReviewService
{
    Task<ContainerReview> GetRevisionContainerReview(Guid id);
    Task<List<ContainerReview>> GetRevisionContainerReviews();
    Task<ContainerReview?> GetContainerReviewForContainer(Guid revisionContainerId);
    Task<ContainerReview> CreateContainerReview(ContainerReview review, Guid azureUniqueId);
}
