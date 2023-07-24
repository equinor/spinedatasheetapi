using datasheetapi.Adapters;
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

    public async Task<RevisionContainerReview?> GetTagDataReview(Guid id)
    {
        var review = await _reviewRepository.GetTagDataReview(id);
        return review;
    }

    public async Task<RevisionContainerReviewDto?> GetTagDataReviewDto(Guid id)
    {
        var review = await _reviewRepository.GetTagDataReview(id);
        return review.ToDtoOrNull();
    }

    public async Task<List<RevisionContainerReview>> GetTagDataReviews()
    {
        var reviews = await _reviewRepository.GetTagDataReviews();
        return reviews;
    }

    public async Task<List<RevisionContainerReviewDto>> GetTagDataReviewDtos()
    {
        var reviews = await _reviewRepository.GetTagDataReviews();
        return reviews.ToDto();
    }

    public async Task<List<RevisionContainerReview>> GetTagDataReviewsForProject(Guid projectId)
    {
        return await Task.Run(() => new List<RevisionContainerReview>());
    }

    public async Task<List<RevisionContainerReviewDto>> GetTagDataReviewDtosForProject(Guid projectId)
    {
        return await Task.Run(() => new List<RevisionContainerReviewDto>());
    }

    public async Task<List<RevisionContainerReviewDto>> GetReviewDtosForTag(Guid tagId)
    {
        var comments = await _reviewRepository.GetRevisionContainerReviewForRevision(tagId);
        return comments.ToDto();
    }

    public async Task<List<RevisionContainerReview>> GetReviewsForTag(Guid tagId)
    {
        var comments = await _reviewRepository.GetRevisionContainerReviewForRevision(tagId);
        return comments;
    }

    public async Task<RevisionContainerReviewDto> CreateTagDataReview(RevisionContainerReviewDto review, Guid azureUniqueId)
    {
        review.ApproverId = azureUniqueId;

        var tagData = await _tagDataService.GetAllTagData();
        var revisionContainer = tagData.FirstOrDefault(td => td.RevisionContainer?.Id == review.RevisionContainerId)?.RevisionContainer ?? throw new Exception("Invalid revision");

        var reviewModel = review.ToModelOrNull();

        if (reviewModel is null) { throw new Exception("Invalid review"); }

        revisionContainer.RevisionContainerReview = reviewModel;

        RevisionContainerReview? savedReview = await _reviewRepository.AddTagDataReview(reviewModel) ?? throw new Exception("Invalid comment");
        return savedReview?.ToDtoOrNull() ?? throw new Exception("Saving revision container review failed");
    }
}
