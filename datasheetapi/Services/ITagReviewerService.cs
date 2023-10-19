namespace datasheetapi.Services
{
    public interface ITagReviewerService
    {
        Task<List<TagReviewer>> CreateReviewers(Guid reviewId, List<TagReviewer> reviewers);
        Task<TagReviewer> UpdateReviewer(Guid reviewerId, Guid userFromToken, ReviewStateEnum reviewStatus);
    }
}
