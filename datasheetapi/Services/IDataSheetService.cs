namespace datasheetapi.Services;

public interface IDatasheetService
{
    Task<DatasheetDto?> GetDatasheetById(Guid id);
    Task<List<DatasheetDto>> GetAllDatasheets();
    Task<ActionResult<List<DatasheetDto>>> GetDatasheetsForContractor(Guid id);
}
