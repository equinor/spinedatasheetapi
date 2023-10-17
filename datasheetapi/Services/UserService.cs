using datasheetapi.Exceptions;

namespace datasheetapi.Services;

public class UserService : IUserService
{
    private readonly IAzureUserCacheService _azureUserCacheService;
    private readonly IFusionService _fusionService;

    public UserService(IAzureUserCacheService azureUserCacheService, IFusionService fusionService)
    {
        _azureUserCacheService = azureUserCacheService;
        _fusionService = fusionService;
    }

    public async Task<string> GetDisplayName(Guid userId)
    {
        var azureUser = await _azureUserCacheService.GetAzureUserAsync(userId);
        if (azureUser == null)
        {
            var user = await _fusionService.ResolveUserFromPersonId(userId);
            if (user != null)
            {
                azureUser = new AzureUser { AzureUniqueId = userId, Name = user?.Name };
                _azureUserCacheService.AddAzureUser(azureUser);
            }
        }
        if (azureUser != null)
        {
            return azureUser.Name ?? "Unknown user";
        }
        else
        {
            throw new NotFoundException("Unable to find the username for the userId: " + userId);
        }
    }

    public async Task<Dictionary<Guid, string>> GetDisplayNames(List<Guid> userIds)
    {
        var userIdUserNameMap = new Dictionary<Guid, string>();
        foreach (Guid userId in userIds)
        {
            var userName = await GetDisplayName(userId);
            userIdUserNameMap.TryAdd(userId, userName);
        }
        return userIdUserNameMap;
    }
}
