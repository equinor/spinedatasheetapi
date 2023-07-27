using datasheetapi.Models;
using datasheetapi.Services;

namespace tests.Services;
public class AzureUserCacheServiceTests
{
    private readonly AzureUserCacheService _azureUserCacheService;

    public AzureUserCacheServiceTests()
    {
        _azureUserCacheService = new AzureUserCacheService();
    }

    [Fact]
    public async Task GetAzureUserAsync_ReturnsNull_WhenCacheIsEmptyAsync()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = await _azureUserCacheService.GetAzureUserAsync(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async void GetAzureUserAsync_ReturnsNull_WhenAzureUserNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var azureUser = new AzureUser { AzureUniqueId = Guid.NewGuid(), Name = "Test User" };
        _azureUserCacheService.AddAzureUser(azureUser);

        // Act
        var result = await _azureUserCacheService.GetAzureUserAsync(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAzureUserAsync_ReturnsAzureUser_WhenAzureUserFoundAsync()
    {
        // Arrange
        var id = Guid.NewGuid();
        var azureUser = new AzureUser { AzureUniqueId = id, Name = "Test User" };
        _azureUserCacheService.AddAzureUser(azureUser);

        // Act
        var result = await _azureUserCacheService.GetAzureUserAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(azureUser, result);
    }

    [Fact]
    public async Task AddAzureUser_AddsAzureUserToCacheAsync()
    {
        // Arrange
        var azureUser = new AzureUser { AzureUniqueId = Guid.NewGuid(), Name = "Test User" };

        // Act
        _azureUserCacheService.AddAzureUser(azureUser);
        var result = await _azureUserCacheService.GetAzureUserAsync(azureUser.AzureUniqueId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(azureUser, result);
    }
}
