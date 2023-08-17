namespace datasheetapi.Services;

public interface IRevisionContainerReviewService
{
    Task<RevisionContainerReview?> GetRevisionContainerReview(Guid id);
    Task<RevisionContainerReviewDto?> GetRevisionContainerReviewDto(Guid id);
    Task<List<RevisionContainerReview>> GetRevisionContainerReviews();
    Task<List<RevisionContainerReviewDto>> GetRevisionContainerReviewDtos();
    Task<RevisionContainerReview?> GetRevisionContainerReviewForRevision(Guid id);
    Task<List<RevisionContainerReview>> GetRevisionContainerReviewsForProject(Guid projectId);
    Task<List<RevisionContainerReviewDto>> GetRevisionContainerReviewDtosForProject(Guid projectId);
    Task<RevisionContainerReviewDto?> GetRevisionContainerReviewDtoForTag(Guid tagId);
    Task<RevisionContainerReview?> GetRevisionContainerReviewForTag(Guid tagId);
    Task<RevisionContainerReviewDto> CreateRevisionContainerReview(RevisionContainerReviewDto review, Guid azureUniqueId);
}
