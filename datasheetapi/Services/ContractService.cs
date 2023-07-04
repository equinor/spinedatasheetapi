using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ContractService : IContractService
{
    private readonly ILogger<ContractService> _logger;
    private readonly IContractRepository _contractRepository;

    public ContractService(ILoggerFactory loggerFactory, 
        IContractRepository contractRepository)
    {
        _logger = loggerFactory.CreateLogger<ContractService>();
        _contractRepository = contractRepository;
    }

    public async Task<Contract?> GetContract(Guid id)
    {
        return await _contractRepository.GetContract(id);
    }

    public async Task<List<Contract>> GetContracts()
    {
        return await _contractRepository.GetContracts();
    }

    public async Task<List<Contract>> GetContractsForContractor(Guid contractorId)
    {
        return await _contractRepository.GetContractForContractor(contractorId);
    }

    public async Task<List<Contract>> GetContractsForProject(Guid projectId)
    {
        return await _contractRepository.GetContractForProject(projectId);
    }
}
