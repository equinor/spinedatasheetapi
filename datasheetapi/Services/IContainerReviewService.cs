namespace datasheetapi.Services;

public interface IContainerReviewService
{
    Task<ContainerReview> GetContainerReview(Guid id);
    Task<List<ContainerReview>> GetContainerReviews();
    Task<ContainerReview?> GetContainerReviewForContainer(Guid revisionContainerId);
    Task<ContainerReview> CreateContainerReview(ContainerReview review, Guid azureUniqueId);
}
