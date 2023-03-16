using datasheetapi.Models;

namespace api.Services;

public interface IDatasheetService
{
    Datasheet GetDatasheet(Guid id);
    List<Datasheet> GetDatasheetsForContractor(Guid contractorId);
    List<Datasheet> GetDatasheets();
}
