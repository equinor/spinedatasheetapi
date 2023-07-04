namespace datasheetapi.Repositories;

public class DummyContractRepository : IContractRepository
{
    private readonly List<Contract> _contracts = new();
    private readonly ILogger<DummyContractRepository> _logger;

    public DummyContractRepository(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DummyContractRepository>();
    }

    public async Task<Contract?> GetContract(Guid id)
    {
        return await Task.Run(() => _contracts.Find(c => c.Id == id));
    }

    public async Task<List<Contract>> GetContracts()
    {
        return await Task.Run(() => _contracts);
    }

    public async Task<List<Contract>> GetContractForContractor(Guid contractorId)
    {
        return await Task.Run(() => _contracts.Where(c => c.ContractorId == contractorId).ToList());
    }

        public async Task<List<Contract>> GetContractForProject(Guid projectId)
    {
        return await Task.Run(() => _contracts.Where(c => c.ProjectId == projectId).ToList());
    }
}
