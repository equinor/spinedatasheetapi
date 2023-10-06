namespace datasheetapi.Repositories
{
    public interface IReviewerRepository
    {
        Task<List<Reviewer>> CreateReviewers(List<Reviewer> review);
        Task<Reviewer> UpdateReviewer(Reviewer reviewer);
        Task<Reviewer?> GetReviewer(Guid reviewId, Guid reviewerId);
    }
}
