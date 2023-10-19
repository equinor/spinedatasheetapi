namespace datasheetapi.Repositories;

public interface IContainerReviewRepository
{
    Task<ContainerReview?> GetContainerReview(Guid reviewId);
    Task<List<ContainerReview>> GetContainerReviews();
    Task<ContainerReview?> GetContainerReviewForContainer(Guid revisionContainerId);
    Task<ContainerReview> AddContainerReview(ContainerReview review);
}
