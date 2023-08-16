using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class RevisionContainerRepository : IRevisionContainerRepository
{
    private readonly ILogger<RevisionContainerRepository> _logger;
    private readonly DatabaseContext _context;

    public RevisionContainerRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<RevisionContainerRepository>();
        _context = context;
    }

    public async Task<RevisionContainer?> GetRevisionContainer(Guid id)
    {
        var revisionContainer = await _context.RevisionContainers.FindAsync(id);
        return revisionContainer;
    }

    public async Task<RevisionContainer?> GetRevisionContainerForTagNo(string tagNo)
    {
        var revisionContainerTagNo = await _context.RevisionContainerTagNos.Include(x => x.RevisionContainer).FirstOrDefaultAsync(x => x.TagNo == tagNo);
        if (revisionContainerTagNo == null) return null;
        var revisionContainer = revisionContainerTagNo.RevisionContainer;
        return revisionContainer;
    }

    public async Task<List<RevisionContainer>> GetRevisionContainers()
    {
        var revisionContainers = await _context.RevisionContainers.ToListAsync();
        return revisionContainers;
    }

    public async Task<List<RevisionContainer>> GetRevisionContainersForContract(Guid contractId)
    {
        var revisionContainers = await _context.RevisionContainers.Where(c => c.ContractId == contractId).ToListAsync();
        return revisionContainers;
    }
}
