using datasheetapi.Models;

namespace api.Services;

public class ContractorService : IContractorService
{
    private readonly ILogger<DatasheetService> _logger;

    public ContractorService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DatasheetService>();
    }

    public Contractor GetContractor(Guid id)
    {
        return new Contractor();
    }

    public List<Contractor> GetContractors()
    {
        return new List<Contractor>();
    }

    public List<Contractor> GetContractorsForProject(Guid projectId)
    {
        return new List<Contractor>();
    }
}
