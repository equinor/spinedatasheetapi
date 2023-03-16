using datasheetapi.Models;

namespace api.Services;

public interface IContractorService
{
    Contractor GetContractor(Guid id);
    List<Contractor> GetContractors();
    List<Contractor> GetContractorsForProject(Guid projectId);
}
