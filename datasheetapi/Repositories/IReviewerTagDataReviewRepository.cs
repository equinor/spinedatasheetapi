namespace datasheetapi.Repositories
{
    public interface IReviewerTagDataReviewRepository
    {
        Task<ReviewerTagDataReview> CreateReviewerTagDataReview(ReviewerTagDataReview review);
    }
}
