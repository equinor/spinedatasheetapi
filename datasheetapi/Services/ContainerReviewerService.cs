using datasheetapi.Adapters;
using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ContainerReviewerService
{
    private readonly ILogger<ContainerReviewService> _logger;
    private readonly IContainerService _containerService;

    private readonly ContainerReviewerRepository _containerReviewerRepository;

    public ContainerReviewerService(ILoggerFactory loggerFactory,
        ContainerReviewerRepository containerReviewerRepository,
        IContainerService containerService)
    {
        _containerReviewerRepository = containerReviewerRepository;
        _containerService = containerService;
        _logger = loggerFactory.CreateLogger<ContainerReviewService>();
    }

    public async Task<ContainerReviewer> GetContainerReviewer(Guid containerReviewId)
    {
        var review = await _containerReviewerRepository.GetContainerReviewer(containerReviewId) ??
            throw new NotFoundException($"Unable to find container review - {containerReviewId}.");
        return review;
    }

    public async Task<List<ContainerReviewer>> GetContainerReviewers()
    {
        var reviews = await _containerReviewerRepository.GetContainerReviewers();
        return reviews;
    }

    public async Task<List<ContainerReviewer>> GetContainerReviewersForContainerReview(Guid containerReviewId, Guid userId)
    {
        var reviews = await _containerReviewerRepository.GetContainerReviewersForContainerReview(containerReviewId, userId);
        return reviews;
    }

    public Task<ContainerReviewer> CreateContainerReviewer(
        ContainerReview review, Guid azureUniqueId)
    {
        throw new NotImplementedException();
        //var _ = await _containerService.GetContainer(review.ContainerId) ??
        //    throw new BadRequestException($"Invalid revision container id - {review.ContainerId}.");

        //return await _containerReviewerRepository.AddContainerReviewer(review);
    }
}
