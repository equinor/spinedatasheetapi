namespace datasheetapi.Repositories;

public interface IContainerRepository
{
    Task<Container?> GetContainer(Guid id);
    Task<List<Container>> GetContainers();
    Task<List<Container>> GetContainersForContract(Guid contractId);
}
