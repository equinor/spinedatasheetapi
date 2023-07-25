namespace datasheetapi.Services;

public interface IRevisionContainerService
{
    Task<RevisionContainer?> GetRevisionContainer(Guid id);
    Task<List<RevisionContainer>> GetRevisionContainers();
    Task<List<RevisionContainer>> GetRevisionContainersForContract(Guid tagId);
}
