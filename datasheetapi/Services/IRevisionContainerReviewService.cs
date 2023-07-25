namespace datasheetapi.Services;

public interface IRevisionContainerReviewService
{
    Task<RevisionContainerReview?> GetRevisionContainerReview(Guid id);
    Task<RevisionContainerReviewDto?> GetRevisionContainerReviewDto(Guid id);
    Task<List<RevisionContainerReview>> GetRevisionContainerReviews();
    Task<List<RevisionContainerReviewDto>> GetRevisionContainerReviewDtos();
    Task<List<RevisionContainerReview>> GetRevisionContainerReviewsForProject(Guid projectId);
    Task<List<RevisionContainerReviewDto>> GetRevisionContainerReviewDtosForProject(Guid projectId);
    Task<List<RevisionContainerReviewDto>> GetRevisionContainerReviewDtosForTag(Guid tagId);
    Task<List<RevisionContainerReview>> GetRevisionContainerReviewsForTag(Guid tagId);
    Task<RevisionContainerReviewDto> CreateRevisionContainerReview(RevisionContainerReviewDto review, Guid azureUniqueId);
}