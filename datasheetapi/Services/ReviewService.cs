namespace datasheetapi.Services;

public class ReviewService
{
    private readonly ILogger<ReviewService> _logger;

    public ReviewService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ReviewService>();
    }

    public async Task<Contract> GetContract(Guid id)
    {
        return await Task.Run(() => new Contract());
    }

    public async Task<List<Contract>> GetContracts()
    {
        return await Task.Run(() => new List<Contract>());
    }

    public async Task<List<Contract>> GetContractsForContractor(Guid contractorId)
    {
        return await Task.Run(() => new List<Contract>());
    }
}
