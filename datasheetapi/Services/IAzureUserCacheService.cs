namespace datasheetapi.Services
{
    public interface IAzureUserCacheService
    {
        public Task<AzureUser?> GetAzureUserAsync(Guid id);
        void AddAzureUser(AzureUser azureUser);
    }
}
