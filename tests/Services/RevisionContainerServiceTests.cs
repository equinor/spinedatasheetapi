using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace tests.Services;
public class RevisionContainerServiceTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly Mock<IRevisionContainerRepository> _revisionContainerRepositoryMock;
    private readonly RevisionContainerService _revisionContainerService;

    public RevisionContainerServiceTests()
    {
        _loggerFactoryMock = new Mock<ILoggerFactory>();
        _revisionContainerRepositoryMock = new Mock<IRevisionContainerRepository>();
        _revisionContainerService = new RevisionContainerService(_loggerFactoryMock.Object, _revisionContainerRepositoryMock.Object);
    }

    [Fact]
    public async Task GetRevisionContainer_ReturnsNull_WhenContainerNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _revisionContainerRepositoryMock.Setup(x => x.GetRevisionContainer(id)).ReturnsAsync((RevisionContainer?)null);

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
        var container = new RevisionContainer { Id = id };
        _revisionContainerRepositoryMock.Setup(x => x.GetRevisionContainer(id)).ReturnsAsync(container);

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
        var containers = new List<RevisionContainer> { new RevisionContainer(), new RevisionContainer() };
        _revisionContainerRepositoryMock.Setup(x => x.GetRevisionContainers()).ReturnsAsync(containers);

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
        var containers = new List<RevisionContainer> { new RevisionContainer(), new RevisionContainer() };
        _revisionContainerRepositoryMock.Setup(x => x.GetRevisionContainersForContract(contractId)).ReturnsAsync(containers);

        // Act
        var result = await _revisionContainerService.GetRevisionContainersForContract(contractId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(containers, result);
    }
}
