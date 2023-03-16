using datasheetapi.Models;

namespace api.Services;

public interface IContractorService
{
    Task<Contractor> GetContractor(Guid id);
    Task<List<Contractor>> GetContractors();
    Task<List<Contractor>> GetContractorsForProject(Guid projectId);
}
