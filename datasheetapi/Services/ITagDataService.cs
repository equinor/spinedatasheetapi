namespace datasheetapi.Services;

public interface ITagDataService
{
    Task<TagDataDto?> GetDatasheetById(Guid id);
    Task<List<TagDataDto>> GetAllDatasheets();
    Task<ActionResult<List<TagDataDto>>> GetDatasheetsForProject(Guid id);
}
