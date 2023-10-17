using datasheetapi.Adapters;

namespace datasheetapi.Services;

public class TagDataEnrichmentService : ITagDataEnrichmentService
{
    private readonly IRevisionContainerService _revisionContainerService;
    private readonly ITagDataReviewService _tagDataReviewService;
    private readonly IUserService _userService;

    public TagDataEnrichmentService(
        IRevisionContainerService revisionContainerService,
        ITagDataReviewService tagDataReviewService,
        IUserService userService)
    {
        _revisionContainerService = revisionContainerService;
        _tagDataReviewService = tagDataReviewService;
        _userService = userService;
    }

    public async Task<ITagDataDto> AddRevisionContainerWithReview(ITagDataDto tagDataDto)
    {
        if (tagDataDto.TagNo == null) { return tagDataDto; }
        var revisionContainer = await _revisionContainerService.GetRevisionContainerWithReviewForTagNo(tagDataDto.TagNo);
        tagDataDto.RevisionContainer = revisionContainer.ToDtoOrNull();

        return tagDataDto;
    }

    public async Task<List<ITagDataDto>> AddRevisionContainerWithReview(List<ITagDataDto> tagDataDto)
    {
        foreach (var tag in tagDataDto)
        {
            if (tag.TagNo == null) { continue; }
            var revisionContainer = await _revisionContainerService.GetRevisionContainerWithReviewForTagNo(tag.TagNo);
            tag.RevisionContainer = revisionContainer.ToDtoOrNull();
        }

        return tagDataDto;
    }

    public async Task<ITagDataDto> AddReview(ITagDataDto tagDataDto)
    {
        if (tagDataDto.TagNo == null) { return tagDataDto; }
        var review = await _tagDataReviewService.GetTagDataReviewsForTag(tagDataDto.TagNo);
        var newestReview = review.OrderByDescending(r => r.CreatedDate).FirstOrDefault();

        if (newestReview != null)
        {
            var reviewerIds = newestReview.Reviewers.Select(r => r.ReviewerId).ToList();

            var displayNameMap = await _userService.GetDisplayNames(reviewerIds);


            tagDataDto.Review = newestReview.ToDto(displayNameMap);
        }

        return tagDataDto;
    }

    public async Task<List<ITagDataDto>> AddReview(List<ITagDataDto> tagDataDto)
    {
        var tagDataIds = tagDataDto.Select(t => t.TagNo ?? "").ToList();
        var reviews = await _tagDataReviewService.GetTagDataReviewsForTags(tagDataIds);

        var userIds = reviews.SelectMany(tagReview =>
                        tagReview.Reviewers.Select(p => p.ReviewerId)).ToList();
        var userIdNameMap = await _userService.GetDisplayNames(userIds);

        foreach (var review in reviews)
        {
            var tag = tagDataDto.FirstOrDefault(t => t.TagNo == review.TagNo);
            if (tag != null)
            {
                tag.Review = review.ToDto(userIdNameMap);
            }
        }

        return tagDataDto;
    }
}
