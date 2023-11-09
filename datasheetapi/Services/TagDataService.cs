using datasheetapi.Exceptions;

namespace datasheetapi.Services;

public class TagDataService : ITagDataService
{
    private readonly IFAMService _FAMService;

    public TagDataService(IFAMService FAMService)
    {
        _FAMService = FAMService;
    }

    public async Task<ITagData> GetTagDataByTagNo(string tagNo)
    {
        var tagData = await _FAMService.GetTagData(tagNo);

        return tagData ?? throw new NotFoundException($"Unable to find Tag Data for Tag No - {tagNo}.");
    }

    public async Task<List<ITagData>> GetTagDataByTagNos(List<string> tagNo)
    {
        var tagData = await _FAMService.GetTagDataForTagNos(tagNo);

        return tagData;
    }

    public async Task<List<ITagData>> GetAllTagData()
    {
        var allTagData = await _FAMService.GetTagData();
        return allTagData;
    }
}
