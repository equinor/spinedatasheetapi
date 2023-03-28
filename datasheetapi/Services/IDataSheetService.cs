namespace datasheetapi.Services;

public interface IDataSheetService
{
    Task<DataSheetDto> GetDataSheetById(Guid id);
    Task<IEnumerable<DataSheetDto>> GetAllDatasheets();
    Task<ActionResult<List<DataSheetDto>>> GetDatasheetsForContractor(Guid id);
}
