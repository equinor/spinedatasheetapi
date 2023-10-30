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

    public async Task<TagReviewer> UpdateTagReviewer(Guid reviewerId, Guid userFromToken,
        TagReviewerStateEnum tagReviewerStatus)
    {

        var existingReviewer = await _tagReviewerRepository.GetTagReviewer(reviewerId)
                               ?? throw new NotFoundException($"Reviewer with reviewerId {reviewerId} not found");

        if (existingReviewer.UserId != userFromToken)
        {
            throw new BadRequestException("Reviewer cannot update other people's reviewer state");
        }

        existingReviewer.State = tagReviewerStatus;

        var result = await _tagReviewerRepository.UpdateTagReviewer(existingReviewer);
        return result;
    }
}
