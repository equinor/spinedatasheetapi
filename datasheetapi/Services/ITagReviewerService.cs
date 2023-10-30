namespace datasheetapi.Services
{
    public interface ITagReviewerService
    {
        Task<List<TagReviewer>> CreateReviewers(Guid reviewId, List<TagReviewer> tagReviewers);
        Task<TagReviewer> UpdateReviewer(Guid reviewerId, Guid userFromToken, ReviewStateEnum reviewStatus);
    }
}
