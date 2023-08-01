using datasheetapi.Adapters;

namespace datasheetapi.Services;

public class TagDataService : ITagDataService
{
    private readonly IFAMService _FAMService;
    private readonly IRevisionContainerService _revisionContainerService;
    private readonly ITagDataReviewService _tagDataReviewService;

    public TagDataService(IFAMService FAMService, IRevisionContainerService revisionContainerService, ITagDataReviewService tagDataReviewService)
    {
        _FAMService = FAMService;
        _revisionContainerService = revisionContainerService;
        _tagDataReviewService = tagDataReviewService;
    }

    public async Task<ITagDataDto?> GetTagDataDtoById(Guid id)
    {
        var tagData = await _FAMService.GetTagData(id);

        if (tagData == null)
        {
            return null;
        }

        return tagData.ToDtoOrNull();
    }

    public async Task<ITagData?> GetTagDataById(Guid id)
    {
        var tagData = await _FAMService.GetTagData(id);

        return tagData;
    }

    public async Task<List<ITagDataDto>> GetAllTagDataDtos(bool includeRevisionContainer = false, bool includeReview = false)
    {
        var allTagData = await _FAMService.GetTagData();
        var allTagDataDtos = allTagData.ToDto();

        if (includeRevisionContainer)
        {
            allTagDataDtos = await AddRevisionContainer(allTagDataDtos);
        }

        if (includeReview)
        {
            allTagDataDtos = await AddReview(allTagDataDtos);
        }

        return allTagDataDtos;
    }

    private async Task<List<ITagDataDto>> AddRevisionContainer(List<ITagDataDto> tagDataDto)
    {
        foreach (var tag in tagDataDto)
        {
            var revisionContainer = await _revisionContainerService.GetRevisionContainerForTagDataId(tag.Id);
            tag.RevisionContainer = revisionContainer.ToDtoOrNull();
        }

        return tagDataDto;
    }

    private async Task<List<ITagDataDto>> AddReview(List<ITagDataDto> tagDataDto)
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

    public async Task<List<ITagData>> GetAllTagData()
    {
        var allTagData = await _FAMService.GetTagData();

        return allTagData;
    }

    public async Task<List<ITagDataDto>> GetTagDataDtosForProject(Guid id)
    {
        var tagDataDtos = new List<ITagDataDto>();
        var tagDataForProject = await _FAMService.GetTagDataForProject(id);
        foreach (var tagData in tagDataForProject)
        {
            tagDataDtos.Add(tagData.ToDto());
        }

        return tagDataDtos;
    }
}
