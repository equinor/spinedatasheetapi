namespace api.Services;

public interface IContractService
{
    Task<Contract> GetContract(Guid contractId);
    Task<List<Contract>> GetContracts();
    Task<List<Contract>> GetContractsForProject(Guid projectId);
}
