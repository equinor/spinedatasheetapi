namespace datasheetapi.Services;

public interface IDummyFAMService
{
    Task<Datasheet?> GetDatasheet(Guid id);
    Task<List<Datasheet>> GetDatasheets();
    Task<List<Datasheet>> GetDatasheetsForProject(Guid projectId);
}
