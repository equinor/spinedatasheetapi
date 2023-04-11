namespace datasheetapi.Services;

public class ContractService : IContractService
{
    private readonly ILogger<ContractService> _logger;

    public ContractService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ContractService>();
    }

    public async Task<Contract> GetContract(Guid id)
    {
        return await Task.Run(() => new Contract());
    }

    public async Task<List<Contract>> GetContracts()
    {
        return await Task.Run(() => new List<Contract>());
    }

    public async Task<List<Contract>> GetContractsForContractor(Guid contractorId)
    {
        return await Task.Run(() => new List<Contract>());
    }
}
