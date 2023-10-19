using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ContainerService : IContainerService
{
    private readonly ILogger<ContainerService> _logger;
    private readonly IContainerRepository _containerRepository;

    public ContainerService(ILoggerFactory loggerFactory, IContainerRepository containerRepository)
    {
        _containerRepository = containerRepository;
        _logger = loggerFactory.CreateLogger<ContainerService>();
    }

    public async Task<Container?> GetContainer(Guid id)
    {
        var revisionContainer = await _containerRepository.GetContainer(id);
        return revisionContainer;
    }

    public async Task<Container?> GetContainerWithReviewForTagNo(string tagNo)
    {
        var revisionContainer = await _containerRepository.GetContainerWithReviewForTagNo(tagNo);
        return revisionContainer;
    }

    public async Task<List<Container>> GetContainers()
    {
        var revisionContainer = await _containerRepository.GetContainers();
        return revisionContainer;
    }

    public async Task<List<Container>> GetContainersForContract(Guid tagId)
    {
        var revisionContainer = await _containerRepository.GetContainersForContract(tagId);
        return revisionContainer;
    }
}
