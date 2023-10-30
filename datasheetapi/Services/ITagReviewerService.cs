namespace datasheetapi.Services
{
    public interface ITagReviewerService
    {
        Task<List<TagReviewer>> CreateReviewers(Guid reviewId, List<TagReviewer> tagReviewers);
        Task<TagReviewer> UpdateTagReviewer(Guid reviewerId, Guid userFromToken, TagReviewerStateEnum reviewStatus);
    }
}
