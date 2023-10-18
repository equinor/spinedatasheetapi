namespace datasheetapi.Services;

public interface IRevisionContainerService
{
    Task<Container?> GetRevisionContainer(Guid id);
    Task<Container?> GetRevisionContainerWithReviewForTagNo(string tagNo);
    Task<List<Container>> GetRevisionContainers();
    Task<List<Container>> GetRevisionContainersForContract(Guid tagId);
}
