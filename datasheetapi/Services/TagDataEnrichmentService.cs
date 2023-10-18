using datasheetapi.Adapters;

namespace datasheetapi.Services;

public class TagDataEnrichmentService : ITagDataEnrichmentService
{
    private readonly IRevisionContainerService _revisionContainerService;
    private readonly IUserService _userService;

    public TagDataEnrichmentService(
        IRevisionContainerService revisionContainerService,
        IUserService userService)
    {
        _revisionContainerService = revisionContainerService;
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
}
