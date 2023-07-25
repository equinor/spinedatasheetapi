using datasheetapi.Adapters;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace tests;
public class ProjectServiceTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly ProjectService _projectService;

    public ProjectServiceTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _projectService = new ProjectService(Mock.Of<ILoggerFactory>(), _projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GetProject_ReturnsProject_WhenValidId()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = new Project { Id = projectId };
        _projectRepositoryMock.Setup(x => x.GetProject(projectId)).ReturnsAsync(project);

        // Act
        var result = await _projectService.GetProject(projectId);

        // Assert
        Assert.Equal(project, result);
        _projectRepositoryMock.Verify(x => x.GetProject(projectId), Times.Once);
    }

    [Fact]
    public async Task GetProject_ReturnsNull_WhenInvalidId()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        _projectRepositoryMock.Setup(x => x.GetProject(projectId)).ReturnsAsync((Project)null);

        // Act
        var result = await _projectService.GetProject(projectId);

        // Assert
        Assert.Null(result);
        _projectRepositoryMock.Verify(x => x.GetProject(projectId), Times.Once);
    }

    [Fact]
    public async Task GetProjectDto_ReturnsProjectDto_WhenValidId()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = new Project { Id = projectId };
        _projectRepositoryMock.Setup(x => x.GetProject(projectId)).ReturnsAsync(project);

        // Act
        var result = await _projectService.GetProjectDto(projectId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(project.ToDtoOrNull().Id, result.Id);
        _projectRepositoryMock.Verify(x => x.GetProject(projectId), Times.Once);
    }

    [Fact]
    public async Task GetProjectDto_ReturnsNull_WhenInvalidId()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        _projectRepositoryMock.Setup(x => x.GetProject(projectId)).ReturnsAsync((Project)null);

        // Act
        var result = await _projectService.GetProjectDto(projectId);

        // Assert
        Assert.Null(result);
        _projectRepositoryMock.Verify(x => x.GetProject(projectId), Times.Once);
    }

    [Fact]
    public async Task GetProjects_ReturnsListOfProjects()
    {
        // Arrange
        var projects = new List<Project> { new Project { Id = Guid.NewGuid() }, new Project { Id = Guid.NewGuid() } };
        _projectRepositoryMock.Setup(x => x.GetProjects()).ReturnsAsync(projects);

        // Act
        var result = await _projectService.GetProjects();

        // Assert
        Assert.Equal(projects, result);
        _projectRepositoryMock.Verify(x => x.GetProjects(), Times.Once);
    }
}
