namespace datasheetapi.Services;

public class AzureUserCacheService : IAzureUserCacheService
{
    private readonly List<AzureUser> _azureUsers = new();

    public AzureUserCacheService()
    {
    }

    public AzureUser? GetAzureUser(Guid id)
    {
        return _azureUsers.FirstOrDefault(x => x.AzureUniqueId == id);
    }

    public void AddAzureUser(AzureUser azureUser)
    {
        _azureUsers.Add(azureUser);
    }
}
