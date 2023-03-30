namespace api.Services;

public interface IContractService
{
    Task<Contract> GetContract(Guid id);
    Task<List<Contract>> GetContracts();
    Task<List<Contract>> GetContractsForContractor(Guid contractorId);
}
