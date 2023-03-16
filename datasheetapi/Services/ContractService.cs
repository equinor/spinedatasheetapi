using datasheetapi.Models;

namespace api.Services;

public class ContractService : IContractService
{
    private readonly ILogger<DatasheetService> _logger;

    public ContractService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DatasheetService>();
    }

    public Contract GetContract(Guid id)
    {
        return new Contract();
    }

    public List<Contract> GetContracts()
    {
        return new List<Contract>();
    }

    public List<Contract> GetContractsForContractor(Guid contractorId)
    {
        return new List<Contract>();
    }
}
