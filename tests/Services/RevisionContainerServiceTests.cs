using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace tests.Services;
public class RevisionContainerServiceTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly Mock<IContainerRepository> _revisionContainerRepositoryMock;
    private readonly RevisionContainerService _revisionContainerService;

    public RevisionContainerServiceTests()
    {
        _loggerFactoryMock = new Mock<ILoggerFactory>();
        _revisionContainerRepositoryMock = new Mock<IContainerRepository>();
        _revisionContainerService = new RevisionContainerService(_loggerFactoryMock.Object, _revisionContainerRepositoryMock.Object);
    }

    [Fact]
    public async Task GetRevisionContainer_ReturnsNull_WhenContainerNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _revisionContainerRepositoryMock.Setup(x => x.GetContainer(id)).ReturnsAsync((Container?)null);

        // Act
        var result = await _revisionContainerService.GetRevisionContainer(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetRevisionContainer_ReturnsContainer_WhenContainerFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var container = new Container { Id = id };
        _revisionContainerRepositoryMock.Setup(x => x.GetContainer(id)).ReturnsAsync(container);

        // Act
        var result = await _revisionContainerService.GetRevisionContainer(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(container, result);
    }

    [Fact]
    public async Task GetRevisionContainers_ReturnsContainers()
    {
        // Arrange
        var containers = new List<Container> { new Container(), new Container() };
        _revisionContainerRepositoryMock.Setup(x => x.GetContainers()).ReturnsAsync(containers);

        // Act
        var result = await _revisionContainerService.GetRevisionContainers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(containers, result);
    }

    [Fact]
    public async Task GetRevisionContainersForContract_ReturnsContainers()
    {
        // Arrange
        var contractId = Guid.NewGuid();
        var containers = new List<Container> { new Container(), new Container() };
        _revisionContainerRepositoryMock.Setup(x => x.GetContainersForContract(contractId)).ReturnsAsync(containers);

        // Act
        var result = await _revisionContainerService.GetRevisionContainersForContract(contractId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(containers, result);
    }
}
