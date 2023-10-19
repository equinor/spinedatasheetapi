using datasheetapi.Adapters;
using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ContainerReviewService : IContainerReviewService
{
    private readonly ILogger<ContainerReviewService> _logger;
    private readonly IContainerService _containerService;

    private readonly IContainerReviewRepository _containerReviewRepository;

    public ContainerReviewService(ILoggerFactory loggerFactory,
        IContainerReviewRepository reviewRepository,
        IContainerService containerService)
    {
        _containerReviewRepository = reviewRepository;
        _containerService = containerService;
        _logger = loggerFactory.CreateLogger<ContainerReviewService>();
    }

    public async Task<ContainerReview> GetContainerReview(Guid containerReviewId)
    {
        var review = await _containerReviewRepository.GetContainerReview(containerReviewId) ??
            throw new NotFoundException($"Unable to find container review - {containerReviewId}.");
        return review;
    }

    public async Task<List<ContainerReview>> GetContainerReviews()
    {
        var reviews = await _containerReviewRepository.GetContainerReviews();
        return reviews;
    }

    public async Task<ContainerReview?> GetContainerReviewForContainer(Guid revisionContainerId)
    {
        var reviews = await _containerReviewRepository.GetContainerReviewForContainer(revisionContainerId);
        return reviews;
    }

    public async Task<ContainerReview> CreateContainerReview(
        ContainerReview review, Guid azureUniqueId)
    {
        var _ = await _containerService.GetRevisionContainer(review.ContainerId) ??
            throw new BadRequestException($"Invalid revision container id - {review.ContainerId}.");

        return await _containerReviewRepository.AddContainerReview(review);
    }
}
