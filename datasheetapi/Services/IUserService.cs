namespace datasheetapi.Services;

public interface IUserService
{
    Task<string> GetDisplayName(Guid userId);
    Task<Dictionary<Guid, string>> GetDisplayNames(List<Guid> userIds);
}
