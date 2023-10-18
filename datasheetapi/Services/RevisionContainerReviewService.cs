using datasheetapi.Adapters;
using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class RevisionContainerReviewService : IRevisionContainerReviewService
{
    private readonly ILogger<RevisionContainerReviewService> _logger;
    private readonly IRevisionContainerService _revisionContainerService;

    private readonly IContainerReviewRepository _reviewRepository;

    public RevisionContainerReviewService(ILoggerFactory loggerFactory,
        IContainerReviewRepository reviewRepository,
        IRevisionContainerService revisionContainerService)
    {
        _reviewRepository = reviewRepository;
        _revisionContainerService = revisionContainerService;
        _logger = loggerFactory.CreateLogger<RevisionContainerReviewService>();
    }

    public async Task<ContainerReview> GetRevisionContainerReview(Guid reviewId)
    {
        var review = await _reviewRepository.GetRevisionContainerReview(reviewId) ??
            throw new NotFoundException($"Unable to find review - {reviewId}.");
        return review;
    }

    public async Task<List<ContainerReview>> GetRevisionContainerReviews()
    {
        var reviews = await _reviewRepository.GetRevisionContainerReviews();
        return reviews;
    }

    public async Task<ContainerReview?> GetContainerReviewForContainer(Guid revisionContainerId)
    {
        var reviews = await _reviewRepository.GetRevisionContainerReviewForContainer(revisionContainerId);
        return reviews;
    }

    public Task<ContainerReview> CreateContainerReview(
        ContainerReview review, Guid azureUniqueId)
    {
        throw new NotImplementedException();
        //var revisionContainer = await _revisionContainerService.GetRevisionContainer(review.ContainerId) ??
        //    throw new BadRequestException($"Invalid revision container id - {review.ContainerId}.");

        //revisionContainer.RevisionContainerReview = review;

        //return await _reviewRepository.AddRevisionContainerReview(review);
    }
}
