namespace datasheetapi.Services;

public class AzureUserCacheService : IAzureUserCacheService
{
    private readonly List<AzureUser> _azureUsers = new();

    public AzureUserCacheService()
    {
    }

    public AzureUser? GetAzureUserAsync(Guid id)
    {
        var azureUser = _azureUsers.FirstOrDefault(x => x.AzureUniqueId == id);

        return azureUser;
    }

    public void AddAzureUser(AzureUser azureUser)
    {
        _azureUsers.Add(azureUser);
    }
}
