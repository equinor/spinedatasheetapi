using System.Reflection.Metadata.Ecma335;

using datasheetapi.Models;

namespace api.Services;

public class ContractorService : IContractorService
{
    private readonly ILogger<DatasheetService> _logger;

    public ContractorService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DatasheetService>();
    }

    public async Task<Contractor> GetContractor(Guid id)
    {
        return await Task.Run(() => new Contractor());
    }

    public async Task<List<Contractor>> GetContractors()
    {
        return await Task.Run( () =>  new List<Contractor>());
    }

    public async Task<List<Contractor>> GetContractorsForProject(Guid projectId)
    {
        return await Task.Run( () =>  new List<Contractor>());
    }
}
