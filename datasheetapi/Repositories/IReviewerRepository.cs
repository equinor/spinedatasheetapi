namespace datasheetapi.Repositories
{
    public interface IReviewerRepository
    {
        Task<List<Reviewer>> CreateReviewers(List<Reviewer> review);
    }
}
