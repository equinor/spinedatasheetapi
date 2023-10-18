using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class RevisionContainerService : IRevisionContainerService
{
    private readonly ILogger<RevisionContainerService> _logger;
    private readonly IContainerRepository _revisionContainerRepository;

    public RevisionContainerService(ILoggerFactory loggerFactory, IContainerRepository revisionContainerRepository)
    {
        _revisionContainerRepository = revisionContainerRepository;
        _logger = loggerFactory.CreateLogger<RevisionContainerService>();
    }

    public async Task<Container?> GetRevisionContainer(Guid id)
    {
        var revisionContainer = await _revisionContainerRepository.GetContainer(id);
        return revisionContainer;
    }

    public async Task<Container?> GetRevisionContainerWithReviewForTagNo(string tagNo)
    {
        var revisionContainer = await _revisionContainerRepository.GetContainerWithReviewForTagNo(tagNo);
        return revisionContainer;
    }

    public async Task<List<Container>> GetRevisionContainers()
    {
        var revisionContainer = await _revisionContainerRepository.GetContainers();
        return revisionContainer;
    }

    public async Task<List<Container>> GetRevisionContainersForContract(Guid tagId)
    {
        var revisionContainer = await _revisionContainerRepository.GetContainersForContract(tagId);
        return revisionContainer;
    }
}
