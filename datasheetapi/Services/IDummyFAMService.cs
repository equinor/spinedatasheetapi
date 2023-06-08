namespace datasheetapi.Services;

public interface IDummyFAMService
{
    Task<TagData?> GetDatasheet(Guid id);
    Task<List<TagData>> GetDatasheets();
    Task<List<TagData>> GetDatasheetsForProject(Guid projectId);
}
