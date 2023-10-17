using datasheetapi.Exceptions;

namespace datasheetapi.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IAzureUserCacheService _azureUserCacheService;
    private readonly IFusionService _fusionService;

    public UserService(
        IAzureUserCacheService azureUserCacheService,
        IFusionService fusionService,
        ILoggerFactory loggerFactory)
    {
        _azureUserCacheService = azureUserCacheService;
        _fusionService = fusionService;
        _logger = loggerFactory.CreateLogger<UserService>();
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
            _logger.LogWarning("Unable to find the username for the userId: " + userId);
            return "Unknown user";
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
