using datasheetapi.Models;

namespace api.Services;

public interface IDatasheetService
{
    Task<Datasheet> GetDatasheet(Guid id);
    Task<List<Datasheet>> GetDatasheetsForContractor(Guid contractorId);
    Task<List<Datasheet>> GetDatasheets();
}
