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

    public async Task<List<ContainerReviewer>> GetContainerReviewers(Guid userId)
    {
        var reviews = await _containerReviewerRepository.GetContainerReviewers(userId);
        return reviews;
    }

    public async Task<List<ContainerReviewer>> GetContainerReviewersForContainerReview(Guid containerReviewId, Guid userId)
    {
        var reviews = await _containerReviewerRepository.GetContainerReviewersForContainerReview(containerReviewId, userId);
        return reviews;
    }

    public async Task<ContainerReviewer> CreateContainerReviewer(Guid containerReviewId, ContainerReviewer review)
    {
        var _ = await _containerService.GetContainerReview(containerReviewId) ??
            throw new BadRequestException($"Invalid container review id - {containerReviewId}.");

        var containerReviewerExists =
            await _containerReviewerRepository.AnyContainerReviewerWithUserIdAndContainerReviewId(review.UserId,
                containerReviewId);

        if (containerReviewerExists)
        {
            throw new ConflictException($"Container reviewer already exists");
        }

        review.ContainerReviewId = containerReviewId;

        return await _containerReviewerRepository.CreateContainerReviewer(review);
    }

    public async Task<ContainerReviewer> UpdateContainerReviewer(Guid reviewerId, Guid userFromToken,
        ContainerReviewerStateEnum containerReviewerStatus)
    {

        var existingReviewer = await _containerReviewerRepository.GetContainerReviewer(reviewerId)
                               ?? throw new NotFoundException($"Container reviewer with Id: {reviewerId} not found");

        if (existingReviewer.UserId != userFromToken)
        {
            throw new BadRequestException("Reviewer cannot update other people's container state");
        }

        existingReviewer.State = containerReviewerStatus;

        var result = await _containerReviewerRepository.UpdateContainerReviewer(existingReviewer);
        return result;
    }
}
