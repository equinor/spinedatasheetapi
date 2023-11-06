using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class ContainerRepository : IContainerRepository
{
    private readonly ILogger<ContainerRepository> _logger;
    private readonly DatabaseContext _context;

    public ContainerRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<ContainerRepository>();
        _context = context;
    }

    public async Task<Container?> GetContainer(Guid id)
    {
        var revisionContainer = await _context.Containers.Include(c => c.Tags).FirstOrDefaultAsync();
        return revisionContainer;
    }

    public async Task<List<Container>> GetContainers()
    {
        var revisionContainers = await _context.Containers.Include(c => c.Tags).ToListAsync();
        return revisionContainers;
    }

    public async Task<List<Container>> GetContainersForContract(Guid contractId)
    {
        var revisionContainers = await _context.Containers.Where(c => c.ContractId == contractId).ToListAsync();
        return revisionContainers;
    }
}
