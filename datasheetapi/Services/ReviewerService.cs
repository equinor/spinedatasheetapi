using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class ReviewerService : IReviewerService
{

    private readonly IReviewerRepository _reviewerRepository;

    public ReviewerService(
        IReviewerRepository reviewerRepository)
    {
        _reviewerRepository = reviewerRepository;
    }

    public async Task<List<TagReviewer>> CreateReviewers(Guid reviewId, List<TagReviewer> reviewers)
    {
        var result = await _reviewerRepository.CreateReviewers(reviewers);

        return result;
    }

    public async Task<TagReviewer> UpdateReviewer(Guid reviewId, Guid reviewerId, Guid userFromToken, ReviewStateEnum reviewStatus)
    {
        if (reviewerId != userFromToken) { throw new BadRequestException("Reviewer cannot update other people's review"); }

        var existingReviewer = await _reviewerRepository.GetReviewer(reviewId, reviewerId)
            ?? throw new NotFoundException($"Reviewer with reviewId {reviewId} and reviewerId {reviewerId} not found");

        existingReviewer.State = TagReviewerStateEnum.NotReviewed;

        var result = await _reviewerRepository.UpdateReviewer(existingReviewer);
        return result;
    }
}
