namespace datasheetapi.Services
{
    public interface IAzureUserCacheService
    {
        public AzureUser? GetAzureUserAsync(Guid id);
        void AddAzureUser(AzureUser azureUser);
    }
}
