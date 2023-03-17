using datasheetapi.Models;

namespace api.Services;

public interface ICommonLibraryService
{
    Task<List<Contract>> GetContractsFromCommonLibrary();
}
