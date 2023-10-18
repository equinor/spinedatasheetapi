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
        var revisionContainer = await _context.Containers.FindAsync(id);
        return revisionContainer;
    }

    public Task<Container?> GetContainerWithReviewForTagNo(string tagNo)
    {
        throw new NotImplementedException();
        //var containerTagNo = await _context.ContainerTags.Include(x => x.RevisionContainer).ThenInclude(x => x.RevisionContainerReview).FirstOrDefaultAsync(x => x.TagNo == tagNo);
        //if (containerTagNo == null) { return null; }
        //var container = containerTagNo.RevisionContainer;
        //return container;
    }

    public async Task<List<Container>> GetContainers()
    {
        var revisionContainers = await _context.Containers.ToListAsync();
        return revisionContainers;
    }

    public async Task<List<Container>> GetContainersForContract(Guid contractId)
    {
        var revisionContainers = await _context.Containers.Where(c => c.ContractId == contractId).ToListAsync();
        return revisionContainers;
    }
}
