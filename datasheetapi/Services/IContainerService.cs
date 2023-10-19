namespace datasheetapi.Services;

public interface IContainerService
{
    Task<Container?> GetContainer(Guid id);
    Task<Container?> GetContainerWithReviewForTagNo(string tagNo);
    Task<List<Container>> GetContainers();
    Task<List<Container>> GetContainersForContract(Guid tagId);
}
