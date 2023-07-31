using datasheetapi.Adapters;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class RevisionContainerReviewService : IRevisionContainerReviewService
{
    private readonly ILogger<RevisionContainerReviewService> _logger;
    private readonly IRevisionContainerService _revisionContainerService;

    private readonly IRevisionContainerReviewRepository _reviewRepository;

    public RevisionContainerReviewService(ILoggerFactory loggerFactory, IRevisionContainerReviewRepository reviewRepository,
        IRevisionContainerService revisionContainerService)
    {
        _reviewRepository = reviewRepository;
        _revisionContainerService = revisionContainerService;
        _logger = loggerFactory.CreateLogger<RevisionContainerReviewService>();
    }

    public async Task<RevisionContainerReview?> GetRevisionContainerReview(Guid id)
    {
        var review = await _reviewRepository.GetRevisionContainerReview(id);
        return review;
    }

    public async Task<RevisionContainerReviewDto?> GetRevisionContainerReviewDto(Guid id)
    {
        var review = await _reviewRepository.GetRevisionContainerReview(id);
        return review.ToDtoOrNull();
    }

    public async Task<List<RevisionContainerReview>> GetRevisionContainerReviews()
    {
        var reviews = await _reviewRepository.GetRevisionContainerReviews();
        return reviews;
    }

    public async Task<List<RevisionContainerReviewDto>> GetRevisionContainerReviewDtos()
    {
        var reviews = await _reviewRepository.GetRevisionContainerReviews();
        return reviews.ToDto();
    }

    public async Task<List<RevisionContainerReview>> GetRevisionContainerReviewsForProject(Guid projectId)
    {
        return await Task.Run(() => new List<RevisionContainerReview>());
    }

    public async Task<List<RevisionContainerReviewDto>> GetRevisionContainerReviewDtosForProject(Guid projectId)
    {
        return await Task.Run(() => new List<RevisionContainerReviewDto>());
    }

    public async Task<List<RevisionContainerReviewDto>> GetRevisionContainerReviewDtosForTag(Guid tagId)
    {
        var comments = await _reviewRepository.GetRevisionContainerReviewForRevision(tagId);
        return comments.ToDto();
    }

    public async Task<List<RevisionContainerReview>> GetRevisionContainerReviewsForTag(Guid tagId)
    {
        var comments = await _reviewRepository.GetRevisionContainerReviewForRevision(tagId);
        return comments;
    }

    public async Task<RevisionContainerReviewDto> CreateRevisionContainerReview(RevisionContainerReviewDto review, Guid azureUniqueId)
    {
        review.ApproverId = azureUniqueId;



        var revisionContainer = await _revisionContainerService.GetRevisionContainer(review.RevisionContainerId) ?? throw new Exception("Invalid revision container id");

        var reviewModel = review.ToModelOrNull() ?? throw new Exception("Invalid review");
        revisionContainer.RevisionContainerReview = reviewModel;

        RevisionContainerReview savedReview = await _reviewRepository.AddRevisionContainerReview(reviewModel);
        return savedReview.ToDtoOrNull() ?? throw new Exception("Saving revision container review failed");
    }
}
