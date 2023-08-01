namespace datasheetapi.Repositories;

public interface IRevisionContainerRepository
{
    Task<RevisionContainer?> GetRevisionContainer(Guid id);
    Task<RevisionContainer?> GetRevisionContainerForTagDataId(Guid id);
    Task<List<RevisionContainer>> GetRevisionContainers();
    Task<List<RevisionContainer>> GetRevisionContainersForContract(Guid contractId);
}
