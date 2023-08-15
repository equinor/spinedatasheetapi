using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly ILogger<ContractRepository> _logger;
    private readonly DatabaseContext _context;

    public ContractRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<ContractRepository>();
        _context = context;
    }

    public async Task<Contract?> GetContract(Guid id)
    {
        var contract = await _context.Contracts.FindAsync(id);
        return contract;
    }

    public async Task<List<Contract>> GetContracts()
    {
        var contracts = await _context.Contracts.ToListAsync();
        return contracts;
    }

    public async Task<List<Contract>> GetContractForContractor(Guid contractorId)
    {
        var contracts = await _context.Contracts.Where(c => c.ContractorId == contractorId).ToListAsync();
        return contracts;
    }

    public async Task<List<Contract>> GetContractForProject(Guid projectId)
    {
        var contracts = await _context.Contracts.Where(c => c.ProjectId == projectId).ToListAsync();
        return contracts;
    }
}
