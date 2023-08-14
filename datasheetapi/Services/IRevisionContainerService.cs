namespace datasheetapi.Services;

public interface IRevisionContainerService
{
    Task<RevisionContainer?> GetRevisionContainer(Guid id);
    Task<RevisionContainer?> GetRevisionContainerForTagNo(string tagNo);
    Task<List<RevisionContainer>> GetRevisionContainers();
    Task<List<RevisionContainer>> GetRevisionContainersForContract(Guid tagId);
}
