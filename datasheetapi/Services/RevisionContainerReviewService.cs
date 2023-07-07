using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class RevisionContainerReviewService
{
    private readonly ILogger<TagDataReviewService> _logger;
    private readonly ITagDataService _tagDataService;

    private readonly IRevisionContainerReviewRepository _reviewRepository;

    public RevisionContainerReviewService(ILoggerFactory loggerFactory, IRevisionContainerReviewRepository reviewRepository,
        ITagDataService tagDataService)
    {
        _reviewRepository = reviewRepository;
        _tagDataService = tagDataService;
        _logger = loggerFactory.CreateLogger<TagDataReviewService>();
    }

    public async Task<RevisionContainerReviewDto?> GetTagDataReview(Guid id)
    {
        var review = await _reviewRepository.GetTagDataReview(id);
        return review;
    }

    public async Task<List<RevisionContainerReviewDto>> GetTagDataReviews()
    {
        var reviews = await _reviewRepository.GetTagDataReviews();
        return reviews;
    }

    public async Task<List<RevisionContainerReviewDto>> GetTagDataReviewsForProject(Guid projectId)
    {
        return await Task.Run(() => new List<RevisionContainerReviewDto>());
    }

    public async Task<List<RevisionContainerReviewDto>> GetReviewsForTag(Guid tagId)
    {
        var comments = await _reviewRepository.GetRevisionContainerReviewForRevision(tagId);
        return comments;
    }

    public async Task<List<RevisionContainerReviewDto>> GetTagDataReviewsForTags(List<Guid> tagIds)
    {
        var comments = await _reviewRepository.GetRevisionContainerReviewsForRevisions(tagIds);
        return comments;
    }

    public async Task<RevisionContainerReviewDto> CreateTagDataReview(RevisionContainerReviewDto review, Guid azureUniqueId)
    {
        review.ApproverId = azureUniqueId;

        var tagData = await _tagDataService.GetAllTagData();
        var revisionContainer = tagData.FirstOrDefault(td => td.RevisionContainer?.Id == review.RevisionContainerId)?.RevisionContainer ?? throw new Exception("Invalid revision");

        revisionContainer.RevisionContainerReview = review;

        RevisionContainerReview? savedReview = await _reviewRepository.AddTagDataReview(review) ?? throw new Exception("Invalid comment");
        return savedReview;
    }
}
