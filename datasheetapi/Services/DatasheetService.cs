using datasheetapi.Models;

namespace api.Services;

public class DatasheetService : IDatasheetService
{
    private readonly ILogger<DatasheetService> _logger;

    public DatasheetService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DatasheetService>();
    }

    public Datasheet GetDatasheet(Guid id)
    {
        return new Datasheet();
    }

    public List<Datasheet> GetDatasheets()
    {
        return new List<Datasheet>();
    }

    public List<Datasheet> GetDatasheetsForContractor(Guid contractorId)
    {
        return new List<Datasheet>();
    }
}
