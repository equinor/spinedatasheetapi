using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class DummyFAMService : IFAMService
{
    private readonly ILogger<DummyFAMService> _logger;

    private readonly List<ITagData> _tagData;

    public DummyFAMService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DummyFAMService>();
        _tagData = DummyData.GetTagDatas();
    }

    public async Task<ITagData?> GetTagData(string tagNo)
    {
        return await Task.Run(() => _tagData.Find(d => d.TagNo == tagNo));
    }

    public async Task<List<ITagData>> GetTagData()
    {
        return await Task.Run(() => _tagData);
    }

    public async Task<List<ITagData>> GetTagDataForProject(Guid projectId)
    {
        return await Task.Run(() => _tagData);
    }
}
