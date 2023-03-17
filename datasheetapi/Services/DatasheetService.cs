using datasheetapi.Models;

namespace api.Services;

public class DatasheetService : IDatasheetService
{
    private readonly ILogger<DatasheetService> _logger;

    public DatasheetService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DatasheetService>();
    }

    public async Task<Datasheet> GetDatasheet(Guid id)
    {
        return await Task.Run(() => new Datasheet());
    }

    public async Task<List<Datasheet>> GetDatasheets()
    {
        return await Task.Run(() => new List<Datasheet>());
    }

    public async Task<List<Datasheet>> GetDatasheetsForContractor(Guid contractorId)
    {
        return await Task.Run(() => new List<Datasheet>());
    }
}
