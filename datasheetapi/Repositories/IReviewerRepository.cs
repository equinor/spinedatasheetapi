namespace datasheetapi.Repositories
{
    public interface IReviewerRepository
    {
        Task<List<TagReviewer>> CreateReviewers(List<TagReviewer> review);
        Task<TagReviewer> UpdateReviewer(TagReviewer reviewer);
        Task<TagReviewer?> GetReviewer(Guid reviewerId);
    }
}
