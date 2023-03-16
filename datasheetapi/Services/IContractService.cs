using datasheetapi.Models;

namespace api.Services;

public interface IContractService
{
    Contract GetContract(Guid id);
    List<Contract> GetContracts();
    List<Contract> GetContractsForContractor(Guid contractorId);
}
