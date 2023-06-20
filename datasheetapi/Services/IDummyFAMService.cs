namespace datasheetapi.Services;

public interface IDummyFAMService
{
    Task<ITagData?> GetTagData(Guid id);
    Task<List<ITagData>> GetTagData();
    Task<List<ITagData>> GetTagDataForProject(Guid projectId);
}
