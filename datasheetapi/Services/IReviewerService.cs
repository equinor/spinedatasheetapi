namespace datasheetapi.Services
{
    public interface IReviewerService
    {
        Task<List<TagReviewer>> CreateReviewers(Guid reviewId, List<TagReviewer> reviewers);
        Task<TagReviewer> UpdateReviewer(Guid reviewerId, Guid userFromToken, ReviewStateEnum reviewStatus);
    }
}
