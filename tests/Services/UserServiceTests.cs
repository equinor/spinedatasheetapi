using datasheetapi.Exceptions;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Fusion.Integration.Profile;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Moq;

namespace tests.Services;
public class UserServiceTests
{
    private readonly Mock<IAzureUserCacheService> _azureUserCacheServiceMock = new();
    private readonly Mock<IFusionService> _fusionServiceMock = new();

    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(
        new NullLoggerFactory(),
        _azureUserCacheServiceMock.Object,
        _fusionServiceMock.Object);
    }

    public static AzureUser SetUpAzureUser()
    {
        return new AzureUser { AzureUniqueId = Guid.NewGuid(), Name = "some name" };
    }

    [Fact]
    public async Task GetUserName_FetchUserNameFromCacheWhenCacheIsLoaded()
    {

        var userId = Guid.NewGuid();
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(userId))
            .ReturnsAsync(new AzureUser { AzureUniqueId = userId, Name = "Test User" });

        await _userService.GetDisplayName(userId);

        _fusionServiceMock.Verify(x => x.ResolveUserFromPersonId(userId), Times.Never);
        _azureUserCacheServiceMock.Verify(x => x.GetAzureUserAsync(userId), Times.Once);
    }

    [Fact]
    public async Task GetUserName_FetchUserNameFromFusion()
    {
        var user = SetUpAzureUser();

        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(user.AzureUniqueId))
            .ReturnsAsync((AzureUser?)null);

        _fusionServiceMock.Setup(x => x.ResolveUserFromPersonId(user.AzureUniqueId))
            .ReturnsAsync(new FusionPersonProfile(FusionAccountType.Employee,
                "upn", Guid.NewGuid(), user.Name ?? ""));

        var result = await _userService.GetDisplayName(user.AzureUniqueId);

        Assert.NotNull(result);
        Assert.Equal(user.Name, result);
        _fusionServiceMock.Verify(x => x.ResolveUserFromPersonId(user.AzureUniqueId), Times.Once);
        _azureUserCacheServiceMock.Verify(x => x.GetAzureUserAsync(user.AzureUniqueId), Times.Once);
    }

    [Fact]
    public async Task GetUserName_ReturnsUnknownUser_WhenUnableToFindUser()
    {
        var user = SetUpAzureUser();
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(user.AzureUniqueId))
            .ReturnsAsync((AzureUser?)null);

        _fusionServiceMock.Setup(x => x.ResolveUserFromPersonId(user.AzureUniqueId))
            .ReturnsAsync((FusionPersonProfile?)null);

        var result = await _userService.GetDisplayName(user.AzureUniqueId);
        Assert.Equal("Unknown user", result);
    }

    [Fact]
    public async Task GetUserIdUserName_RunsOkay()
    {
        var user = SetUpAzureUser();

        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(user.AzureUniqueId))
            .ReturnsAsync((AzureUser?)null);

        _fusionServiceMock.Setup(x => x.ResolveUserFromPersonId(user.AzureUniqueId))
            .ReturnsAsync(new FusionPersonProfile(FusionAccountType.Employee,
                "upn", Guid.NewGuid(), user.Name ?? ""));

        var result = await _userService.GetDisplayNames(new List<Guid> { user.AzureUniqueId });

        Assert.NotNull(result);
        Assert.Equal(user.Name, result[user.AzureUniqueId]);
    }
}
