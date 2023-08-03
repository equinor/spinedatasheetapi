using datasheetapi.Adapters;

namespace datasheetapi.Services;

public class TagDataEnrichmentService : ITagDataEnrichmentService
{
    private readonly IRevisionContainerService _revisionContainerService;
    private readonly ITagDataReviewService _tagDataReviewService;

    public TagDataEnrichmentService(IRevisionContainerService revisionContainerService, ITagDataReviewService tagDataReviewService)
    {
        _revisionContainerService = revisionContainerService;
        _tagDataReviewService = tagDataReviewService;
    }

    public async Task<ITagDataDto> AddRevisionContainer(ITagDataDto tagDataDto)
    {
        var revisionContainer = await _revisionContainerService.GetRevisionContainerForTagDataId(tagDataDto.Id);
        tagDataDto.RevisionContainer = revisionContainer.ToDtoOrNull();

        return tagDataDto;
    }

    public async Task<List<ITagDataDto>> AddRevisionContainer(List<ITagDataDto> tagDataDto)
    {
        foreach (var tag in tagDataDto)
        {
            var revisionContainer = await _revisionContainerService.GetRevisionContainerForTagDataId(tag.Id);
            tag.RevisionContainer = revisionContainer.ToDtoOrNull();
        }

        return tagDataDto;
    }

    public async Task<ITagDataDto> AddReview(ITagDataDto tagDataDto)
    {
        var review = await _tagDataReviewService.GetTagDataReviewsForTag(tagDataDto.Id);
        var newestReview = review.OrderByDescending(r => r.CreatedDate).FirstOrDefault();
        tagDataDto.Review = newestReview.ToDtoOrNull();

        return tagDataDto;
    }

    public async Task<List<ITagDataDto>> AddReview(List<ITagDataDto> tagDataDto)
    {
        var tagDataIds = tagDataDto.Select(t => t.Id).ToList();
        var reviews = await _tagDataReviewService.GetTagDataReviewsForTags(tagDataIds);

        foreach (var review in reviews)
        {
            var tag = tagDataDto.FirstOrDefault(t => t.Id == review.TagDataId);
            if (tag != null)
            {
                tag.Review = review.ToDtoOrNull();
            }
        }

        return tagDataDto;
    }
}
