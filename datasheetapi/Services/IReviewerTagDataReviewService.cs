namespace datasheetapi.Services
{
    public interface IReviewerTagDataReviewService
    {
        Task<ReviewerTagDataReview> CreateReviewerTagDataReview(Guid reviewId, ReviewerTagDataReview review);
    }
}