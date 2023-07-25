using datasheetapi.Dtos;
using datasheetapi.Models;
using datasheetapi.Adapters;

namespace tests.Adapters;
public class ProjectAdapterTests
{
    [Fact]
    public void ToDtoOrNull_WithNullProject_ReturnsNull()
    {
        // Arrange
        Project? project = null;

        // Act
        var result = project.ToDtoOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToDtoOrNull_WithNonNullProject_ReturnsProjectDto()
    {
        // Arrange
        var project = new Project
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            Contracts = new List<Contract>(),
        };

        // Act
        var result = project.ToDtoOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(project.Id, result.Id);
        Assert.Equal(project.CreatedDate, result.CreatedDate);
        Assert.Equal(project.ModifiedDate, result.ModifiedDate);
        Assert.NotNull(result.Contracts);
        Assert.Empty(result.Contracts);
    }

    [Fact]
    public void ToDto_WithNullProjects_ReturnsEmptyList()
    {
        // Arrange
        List<Project>? projects = null;

        // Act
        var result = projects.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToDto_WithNonNullProjects_ReturnsListOfProjectDtos()
    {
        // Arrange
        var projects = new List<Project>
        {
            new Project
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Contracts = new List<Contract>(),
            },
            new Project
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Contracts = new List<Contract>(),
            },
        };

        // Act
        var result = projects.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projects.Count, result.Count);
        for (int i = 0; i < projects.Count; i++)
        {
            Assert.Equal(projects[i].Id, result[i].Id);
            Assert.Equal(projects[i].CreatedDate, result[i].CreatedDate);
            Assert.Equal(projects[i].ModifiedDate, result[i].ModifiedDate);
            Assert.NotNull(result[i].Contracts);
            Assert.Empty(result[i].Contracts);
        }
    }

    [Fact]
    public void ToModelOrNull_WithNullProjectDto_ReturnsNull()
    {
        // Arrange
        ProjectDto? projectDto = null;

        // Act
        var result = projectDto.ToModelOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToModelOrNull_WithNonNullProjectDto_ReturnsProject()
    {
        // Arrange
        var projectDto = new ProjectDto
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            Contracts = new List<ContractDto>(),
        };

        // Act
        var result = projectDto.ToModelOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projectDto.Id, result.Id);
        Assert.Equal(projectDto.CreatedDate, result.CreatedDate);
        Assert.Equal(projectDto.ModifiedDate, result.ModifiedDate);
        Assert.NotNull(result.Contracts);
        Assert.Empty(result.Contracts);
    }

    [Fact]
    public void ToModel_WithNullProjectDtos_ReturnsEmptyList()
    {
        // Arrange
        List<ProjectDto>? projectDtos = null;

        // Act
        var result = projectDtos.ToModel();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToModel_WithNonNullProjectDtos_ReturnsListOfProjects()
    {
        // Arrange
        var projectDtos = new List<ProjectDto>
        {
            new ProjectDto
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Contracts = new List<ContractDto>(),
            },
            new ProjectDto
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Contracts = new List<ContractDto>(),
            },
        };

        // Act
        var result = projectDtos.ToModel();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projectDtos.Count, result.Count);
        for (int i = 0; i < projectDtos.Count; i++)
        {
            Assert.Equal(projectDtos[i].Id, result[i].Id);
            Assert.Equal(projectDtos[i].CreatedDate, result[i].CreatedDate);
            Assert.Equal(projectDtos[i].ModifiedDate, result[i].ModifiedDate);
            Assert.NotNull(result[i].Contracts);
            Assert.Empty(result[i].Contracts);
        }
    }
}