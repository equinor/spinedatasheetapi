namespace datasheetapi.Repositories
{
    public interface ITagReviewerRepository
    {
        Task<List<TagReviewer>> CreateReviewers(List<TagReviewer> review);
        Task<TagReviewer> UpdateTagReviewer(TagReviewer reviewer);
        Task<TagReviewer?> GetTagReviewer(Guid tagReviewerId);
        Task<bool> AnyTagReviewerWithTagNoAndContainerReviewerId(string tagNo, Guid containerReviewerId);
    }
}
