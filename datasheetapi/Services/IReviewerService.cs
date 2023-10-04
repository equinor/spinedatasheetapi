namespace datasheetapi.Services
{
    public interface IReviewerService
    {
        Task<List<Reviewer>> CreateReviewers(Guid reviewId, List<Reviewer> reviewers);
    }
}
