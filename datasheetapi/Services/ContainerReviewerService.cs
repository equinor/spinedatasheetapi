using datasheetapi.Adapters;
using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ContainerReviewerService
{
    private readonly ILogger<ContainerReviewService> _logger;
    private readonly IContainerReviewService _containerService;

    private readonly ContainerReviewerRepository _containerReviewerRepository;

    public ContainerReviewerService(ILoggerFactory loggerFactory,
        ContainerReviewerRepository containerReviewerRepository,
        IContainerReviewService containerService)
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

    public async Task<ContainerReviewer> CreateContainerReviewer(
        ContainerReviewer review)
    {
        var _ = await _containerService.GetContainerReview(review.ContainerReviewId) ??
            throw new BadRequestException($"Invalid container review id - {review.ContainerReviewId}.");

        return await _containerReviewerRepository.AddContainerReviewer(review);
    }
}
