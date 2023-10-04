namespace datasheetapi.Services
{
    public interface IReviewerService
    {
        Task<List<Reviewer>> CreateReviewers(Guid reviewId, List<Reviewer> reviewers);
        Task<Reviewer> UpdateReviewer(Guid reviewId, Guid reviewerId, Guid userFromToken, ReviewStatusEnum reviewStatus);
    }
}
