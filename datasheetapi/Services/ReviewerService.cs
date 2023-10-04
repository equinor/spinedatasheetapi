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

    public async Task<Reviewer> UpdateReviewer(Guid reviewId, Guid reviewerId, Guid userFromToken, ReviewStatusEnum reviewStatus)
    {
        if (reviewerId != userFromToken) { throw new BadRequestException("Reviewer cannot update other people's review"); }

        if (!await _reviewService.AnyTagDataReview(reviewId)) { throw new NotFoundException($"Invalid reviewId - {reviewId}."); }

        var existingReviewer = await _reviewerRepository.GetReviewer(reviewId, reviewerId)
            ?? throw new NotFoundException($"Reviewer with reviewId {reviewId} and reviewerId {reviewerId} not found");

        existingReviewer.Status = reviewStatus;

        var result = await _reviewerRepository.UpdateReviewer(existingReviewer);
        return result;
    }
}
