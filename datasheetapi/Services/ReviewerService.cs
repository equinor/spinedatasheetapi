using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ReviewerService : IReviewerService
{
    private readonly ITagDataReviewService _reviewService;

    private readonly IReviewerRepository _reviewerRepository;

    public ReviewerService(
        ITagDataReviewService reviewService,
        IReviewerRepository reviewerRepository)
    {
        _reviewService = reviewService;
        _reviewerRepository = reviewerRepository;
    }

    public async Task<List<Reviewer>> CreateReviewers(Guid reviewId, List<Reviewer> reviewers)
    {
        if (!await _reviewService.AnyTagDataReview(reviewId)) { throw new NotFoundException($"Invalid reviewId - {reviewId}."); }

        reviewers.ForEach(r => r.TagDataReviewId = reviewId);

        var result = await _reviewerRepository.CreateReviewers(reviewers);

        return result;
    }
}
