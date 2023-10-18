namespace datasheetapi.Repositories;

public interface IContainerReviewRepository
{
    Task<ContainerReview?> GetRevisionContainerReview(Guid reviewId);
    Task<List<ContainerReview>> GetRevisionContainerReviews();
    Task<ContainerReview?> GetRevisionContainerReviewForContainer(Guid revisionContainerId);
    Task<List<ContainerReview>> GetRevisionContainerReviewsForContainers(List<Guid> revisionContainerIds);
    Task<ContainerReview> AddRevisionContainerReview(ContainerReview review);
}
