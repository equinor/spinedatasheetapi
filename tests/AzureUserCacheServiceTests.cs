using datasheetapi.Models;
using datasheetapi.Services;

namespace tests;
public class AzureUserCacheServiceTests
{
    private readonly AzureUserCacheService _azureUserCacheService;

    public AzureUserCacheServiceTests()
    {
        _azureUserCacheService = new AzureUserCacheService();
    }

    [Fact]
    public void GetAzureUserAsync_ReturnsNull_WhenCacheIsEmpty()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = _azureUserCacheService.GetAzureUserAsync(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAzureUserAsync_ReturnsNull_WhenAzureUserNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var azureUser = new AzureUser { AzureUniqueId = Guid.NewGuid(), Name = "Test User" };
        _azureUserCacheService.AddAzureUser(azureUser);

        // Act
        var result = _azureUserCacheService.GetAzureUserAsync(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAzureUserAsync_ReturnsAzureUser_WhenAzureUserFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var azureUser = new AzureUser { AzureUniqueId = id, Name = "Test User" };
        _azureUserCacheService.AddAzureUser(azureUser);

        // Act
        var result = _azureUserCacheService.GetAzureUserAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(azureUser, result);
    }

    [Fact]
    public void AddAzureUser_AddsAzureUserToCache()
    {
        // Arrange
        var azureUser = new AzureUser { AzureUniqueId = Guid.NewGuid(), Name = "Test User" };

        // Act
        _azureUserCacheService.AddAzureUser(azureUser);
        var result = _azureUserCacheService.GetAzureUserAsync(azureUser.AzureUniqueId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(azureUser, result);
    }
}
