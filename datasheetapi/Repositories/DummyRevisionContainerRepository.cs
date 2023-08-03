namespace datasheetapi.Repositories;

public class DummyRevisionContainerRepository : IRevisionContainerRepository
{
    private readonly List<RevisionContainer> _revisionContainer = new();
    private readonly ILogger<DummyRevisionContainerRepository> _logger;

    public DummyRevisionContainerRepository(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DummyRevisionContainerRepository>();
        _revisionContainer = DummyData.GetRevisionContainers();
    }

    public async Task<RevisionContainer?> GetRevisionContainer(Guid id)
    {
        return await Task.Run(() => _revisionContainer.Find(c => c.Id == id));
    }

    public async Task<RevisionContainer?> GetRevisionContainerForTagDataId(Guid id)
    {
        return await Task.Run(() => _revisionContainer.Find(c => c.TagDataIds.Contains(id)));
    }

    public async Task<List<RevisionContainer>> GetRevisionContainers()
    {
        return await Task.Run(() => _revisionContainer);
    }

    public async Task<List<RevisionContainer>> GetRevisionContainersForContract(Guid contractId)
    {
        return await Task.Run(() => _revisionContainer.Where(c => c.ContractId == contractId).ToList());
    }
}
