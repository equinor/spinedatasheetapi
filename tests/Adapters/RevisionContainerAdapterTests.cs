using datasheetapi.Adapters;
using datasheetapi.Dtos;
using datasheetapi.Models;

namespace tests.Adapters;
public class RevisionContainerAdapterTests
{
    [Fact]
    public void ToDtoOrNull_WithNullRevisionContainer_ReturnsNull()
    {
        // Arrange
        Container? revisionContainer = null;

        // Act
        var result = revisionContainer.ToDtoOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToDtoOrNull_WithNonNullRevisionContainer_ReturnsRevisionContainerDto()
    {
        // Arrange
        var revisionContainer = new Container
        {
            Id = Guid.NewGuid(),
            ContainerName = "Test Revision Container",
            RevisionNumber = 1,
            ContainerDate = DateTime.UtcNow,
            Tags = new List<ContainerTags>(),
            ContractId = Guid.NewGuid(),
        };

        // Act
        var result = revisionContainer.ToDtoOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainer.Id, result.Id);
        Assert.Equal(revisionContainer.ContainerName, result.ContainerName);
        Assert.Equal(revisionContainer.RevisionNumber, result.RevisionNumber);
        Assert.Equal(revisionContainer.ContainerDate, result.ContainerDate);
        Assert.Equal(revisionContainer.ContractId, result.ContractId);
    }

    [Fact]
    public void ToDto_WithNullRevisionContainers_ReturnsEmptyList()
    {
        // Arrange
        List<Container>? revisionContainers = null;

        // Act
        var result = revisionContainers.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToDto_WithNonNullRevisionContainers_ReturnsListOfRevisionContainerDtos()
    {
        // Arrange
        var revisionContainers = new List<Container>
        {
            new Container
            {
                Id = Guid.NewGuid(),
                ContainerName = "Test Revision Container 1",
                RevisionNumber = 1,
                ContainerDate = DateTime.UtcNow,
                Tags = new List<ContainerTags>(),
                ContractId = Guid.NewGuid(),
            },
            new Container
            {
                Id = Guid.NewGuid(),
                ContainerName = "Test Revision Container 2",
                RevisionNumber = 2,
                ContainerDate = DateTime.UtcNow,
                Tags = new List<ContainerTags>(),
                ContractId = Guid.NewGuid(),
            },
        };

        // Act
        var result = revisionContainers.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainers.Count, result.Count);
        for (int i = 0; i < revisionContainers.Count; i++)
        {
            Assert.Equal(revisionContainers[i].Id, result[i].Id);
            Assert.Equal(revisionContainers[i].ContainerName, result[i].ContainerName);
            Assert.Equal(revisionContainers[i].RevisionNumber, result[i].RevisionNumber);
            Assert.Equal(revisionContainers[i].ContainerDate, result[i].ContainerDate);
            Assert.Equal(revisionContainers[i].ContractId, result[i].ContractId);
        }
    }

    [Fact]
    public void ToModelOrNull_WithNullRevisionContainerDto_ReturnsNull()
    {
        // Arrange
        ContainerDto? revisionContainerDto = null;

        // Act
        var result = revisionContainerDto.ToModelOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToModelOrNull_WithNonNullRevisionContainerDto_ReturnsRevisionContainer()
    {
        // Arrange
        var revisionContainerDto = new ContainerDto
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            ContainerName = "Test Revision Container",
            RevisionNumber = 1,
            ContainerDate = DateTime.UtcNow,
            ContractId = Guid.NewGuid(),
        };

        // Act
        var result = revisionContainerDto.ToModelOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainerDto.Id, result.Id);
        Assert.Equal(revisionContainerDto.ContainerName, result.ContainerName);
        Assert.Equal(revisionContainerDto.RevisionNumber, result.RevisionNumber);
        Assert.Equal(revisionContainerDto.ContainerDate, result.ContainerDate);
        Assert.Equal(revisionContainerDto.ContractId, result.ContractId);
    }

    [Fact]
    public void ToModel_WithNullRevisionContainerDtos_ReturnsEmptyList()
    {
        // Arrange
        List<ContainerDto>? revisionContainerDtos = null;

        // Act
        var result = revisionContainerDtos.ToModel();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToModel_WithNonNullRevisionContainerDtos_ReturnsListOfRevisionContainers()
    {
        // Arrange
        var revisionContainerDtos = new List<ContainerDto>
        {
            new ContainerDto
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                ContainerName = "Test Revision Container 1",
                RevisionNumber = 1,
                ContainerDate = DateTime.UtcNow,
                ContractId = Guid.NewGuid(),
            },
            new ContainerDto
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                ContainerName = "Test Revision Container 2",
                RevisionNumber = 2,
                ContainerDate = DateTime.UtcNow,
                ContractId = Guid.NewGuid(),
            },
        };

        // Act
        var result = revisionContainerDtos.ToModel();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainerDtos.Count, result.Count);
        for (int i = 0; i < revisionContainerDtos.Count; i++)
        {
            Assert.Equal(revisionContainerDtos[i].Id, result[i].Id);
            Assert.Equal(revisionContainerDtos[i].ContainerName, result[i].ContainerName);
            Assert.Equal(revisionContainerDtos[i].RevisionNumber, result[i].RevisionNumber);
            Assert.Equal(revisionContainerDtos[i].ContainerDate, result[i].ContainerDate);
            Assert.Equal(revisionContainerDtos[i].ContractId, result[i].ContractId);
        }
    }
}
