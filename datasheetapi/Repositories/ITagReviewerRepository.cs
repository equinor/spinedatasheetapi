namespace datasheetapi.Repositories
{
    public interface ITagReviewerRepository
    {
        Task<List<TagReviewer>> CreateReviewers(List<TagReviewer> review);
        Task<TagReviewer> UpdateReviewer(TagReviewer reviewer);
        Task<TagReviewer?> GetReviewer(Guid reviewerId);
        Task<bool> AnyTagReviewerWithTagNoAndContainerReviewerId(string tagNo, Guid containerReviewerId);
    }
}
