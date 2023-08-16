namespace datasheetapi.Services;

public interface IFAMService
{
    Task<ITagData?> GetTagData(string id);
    Task<List<ITagData>> GetTagData();
    Task<List<ITagData>> GetTagDataForProject(Guid projectId);
}
