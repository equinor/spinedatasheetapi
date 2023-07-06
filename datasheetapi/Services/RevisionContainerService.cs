using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class RevisionContainerService
{
    private readonly ILogger<RevisionContainerService> _logger;
    private readonly IRevisionContainerRepository _revisionContainerRepository;

    public RevisionContainerService(ILoggerFactory loggerFactory, IRevisionContainerRepository revisionContainerRepository)
    {
        _revisionContainerRepository = revisionContainerRepository;
        _logger = loggerFactory.CreateLogger<RevisionContainerService>();
    }

    public async Task<RevisionContainer?> GetRevisionContainer(Guid id)
    {
        var revisionContainer = await _revisionContainerRepository.GetRevisionContainer(id);
        return revisionContainer;
    }

    public async Task<List<RevisionContainer>> GetRevisionContainers()
    {
        var revisionContainer = await _revisionContainerRepository.GetRevisionContainers();
        return revisionContainer;
    }

    public async Task<List<RevisionContainer>> GetRevisionContainersForContract(Guid tagId)
    {
        var revisionContainer = await _revisionContainerRepository.GetRevisionContainersForContract(tagId);
        return revisionContainer;
    }
}
