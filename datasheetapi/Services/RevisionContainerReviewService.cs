using datasheetapi.Adapters;
using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class RevisionContainerReviewService : IRevisionContainerReviewService
{
    private readonly ILogger<RevisionContainerReviewService> _logger;
    private readonly IRevisionContainerService _revisionContainerService;

    private readonly IRevisionContainerReviewRepository _reviewRepository;

    public RevisionContainerReviewService(ILoggerFactory loggerFactory,
        IRevisionContainerReviewRepository reviewRepository,
        IRevisionContainerService revisionContainerService)
    {
        _reviewRepository = reviewRepository;
        _revisionContainerService = revisionContainerService;
        _logger = loggerFactory.CreateLogger<RevisionContainerReviewService>();
    }

    public async Task<RevisionContainerReview> GetRevisionContainerReview(Guid reviewId)
    {
        var review = await _reviewRepository.GetRevisionContainerReview(reviewId) ??
            throw new NotFoundException($"Unable to find review - {reviewId}.");
        return review;
    }

    public async Task<List<RevisionContainerReview>> GetRevisionContainerReviews()
    {
        var reviews = await _reviewRepository.GetRevisionContainerReviews();
        return reviews;
    }

    public async Task<RevisionContainerReview?> GetRevisionContainerReviewForContainer(Guid revisionContainerId)
    {
        var reviews = await _reviewRepository.GetRevisionContainerReviewForContainer(revisionContainerId);
        return reviews;
    }

    public async Task<RevisionContainerReview> CreateRevisionContainerReview(
        RevisionContainerReview review, Guid azureUniqueId)
    {
        review.ApproverId = azureUniqueId;

        var revisionContainer = await _revisionContainerService.GetRevisionContainer(review.RevisionContainerId) ??
            throw new BadRequestException($"Invalid revision container id - {review.RevisionContainerId}.");

        revisionContainer.RevisionContainerReview = review;

        return await _reviewRepository.AddRevisionContainerReview(review);
    }
}
