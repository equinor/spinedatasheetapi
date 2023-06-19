namespace datasheetapi.Services
{
    public interface IAzureUserCacheService
    {
        AzureUser? GetAzureUser(Guid id);
        void AddAzureUser(AzureUser azureUser);
    }
}