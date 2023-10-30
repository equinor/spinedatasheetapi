using datasheetapi.Exceptions;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class TagReviewerService : ITagReviewerService
{

    private readonly ITagReviewerRepository _tagReviewerRepository;

    public TagReviewerService(
        ITagReviewerRepository reviewerRepository)
    {
        _tagReviewerRepository = reviewerRepository;
    }

    public async Task<List<TagReviewer>> CreateReviewers(Guid containerReviewerId, List<TagReviewer> tagReviewers)
    {
        tagReviewers.ForEach(tr => tr.ContainerReviewerId = containerReviewerId);

        foreach (var tagReviewer in tagReviewers)
        {
            if (await _tagReviewerRepository.AnyTagReviewerWithTagNoAndContainerReviewerId(tagReviewer.TagNo,
                    containerReviewerId))
            {
                throw new ConflictException($"Tag reviewer for tag {tagReviewer.TagNo} already exists");
            }
        }

        var result = await _tagReviewerRepository.CreateReviewers(tagReviewers);

        return result;
    }

    public async Task<TagReviewer> UpdateReviewer(Guid reviewerId, Guid userFromToken, ReviewStateEnum reviewStatus)
    {
        if (reviewerId != userFromToken) { throw new BadRequestException("Reviewer cannot update other people's review"); }

        var existingReviewer = await _tagReviewerRepository.GetReviewer(reviewerId)
            ?? throw new NotFoundException($"Reviewer with reviewerId {reviewerId} not found");

        existingReviewer.State = TagReviewerStateEnum.NotReviewed;

        var result = await _tagReviewerRepository.UpdateReviewer(existingReviewer);
        return result;
    }
}
