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
        RevisionContainer? revisionContainer = null;

        // Act
        var result = revisionContainer.ToDtoOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToDtoOrNull_WithNonNullRevisionContainer_ReturnsRevisionContainerDto()
    {
        // Arrange
        var revisionContainer = new RevisionContainer
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            RevisionContainerName = "Test Revision Container",
            RevisionNumber = 1,
            RevisionContainerDate = DateTime.UtcNow,
            TagNo = new List<RevisionContainerTagNo>(),
            RevisionContainerReview = new RevisionContainerReview(),
            ContractId = Guid.NewGuid(),
            Contract = null,
        };

        // Act
        var result = revisionContainer.ToDtoOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainer.Id, result.Id);
        Assert.Equal(revisionContainer.CreatedDate, result.CreatedDate);
        Assert.Equal(revisionContainer.ModifiedDate, result.ModifiedDate);
        Assert.Equal(revisionContainer.RevisionContainerName, result.RevisionContainerName);
        Assert.Equal(revisionContainer.RevisionNumber, result.RevisionNumber);
        Assert.Equal(revisionContainer.RevisionContainerDate, result.RevisionContainerDate);
        Assert.NotNull(result.TagData);
        Assert.NotNull(result.RevisionContainerReview);
        Assert.Equal(revisionContainer.ContractId, result.ContractId);
        Assert.Null(result.Contract);
    }

    [Fact]
    public void ToDto_WithNullRevisionContainers_ReturnsEmptyList()
    {
        // Arrange
        List<RevisionContainer>? revisionContainers = null;

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
        var revisionContainers = new List<RevisionContainer>
        {
            new RevisionContainer
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                RevisionContainerName = "Test Revision Container 1",
                RevisionNumber = 1,
                RevisionContainerDate = DateTime.UtcNow,
                TagNo = new List<RevisionContainerTagNo>(),
                RevisionContainerReview = new RevisionContainerReview(),
                ContractId = Guid.NewGuid(),
                Contract = null,
            },
            new RevisionContainer
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                RevisionContainerName = "Test Revision Container 2",
                RevisionNumber = 2,
                RevisionContainerDate = DateTime.UtcNow,
                TagNo = new List<RevisionContainerTagNo>(),
                RevisionContainerReview = new RevisionContainerReview(),
                ContractId = Guid.NewGuid(),
                Contract = null,
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
            Assert.Equal(revisionContainers[i].CreatedDate, result[i].CreatedDate);
            Assert.Equal(revisionContainers[i].ModifiedDate, result[i].ModifiedDate);
            Assert.Equal(revisionContainers[i].RevisionContainerName, result[i].RevisionContainerName);
            Assert.Equal(revisionContainers[i].RevisionNumber, result[i].RevisionNumber);
            Assert.Equal(revisionContainers[i].RevisionContainerDate, result[i].RevisionContainerDate);
            Assert.NotNull(result[i].TagData);
            Assert.NotNull(result[i].RevisionContainerReview);
            Assert.Equal(revisionContainers[i].ContractId, result[i].ContractId);
            Assert.Null(result[i].Contract);
        }
    }

    [Fact]
    public void ToModelOrNull_WithNullRevisionContainerDto_ReturnsNull()
    {
        // Arrange
        RevisionContainerDto? revisionContainerDto = null;

        // Act
        var result = revisionContainerDto.ToModelOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToModelOrNull_WithNonNullRevisionContainerDto_ReturnsRevisionContainer()
    {
        // Arrange
        var revisionContainerDto = new RevisionContainerDto
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            RevisionContainerName = "Test Revision Container",
            RevisionNumber = 1,
            RevisionContainerDate = DateTime.UtcNow,
            TagData = new List<ITagDataDto>(),
            RevisionContainerReview = new RevisionContainerReviewDto(),
            ContractId = Guid.NewGuid(),
            Contract = null,
        };

        // Act
        var result = revisionContainerDto.ToModelOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revisionContainerDto.Id, result.Id);
        Assert.Equal(revisionContainerDto.CreatedDate, result.CreatedDate);
        Assert.Equal(revisionContainerDto.ModifiedDate, result.ModifiedDate);
        Assert.Equal(revisionContainerDto.RevisionContainerName, result.RevisionContainerName);
        Assert.Equal(revisionContainerDto.RevisionNumber, result.RevisionNumber);
        Assert.Equal(revisionContainerDto.RevisionContainerDate, result.RevisionContainerDate);
        Assert.NotNull(result.RevisionContainerReview);
        Assert.Equal(revisionContainerDto.ContractId, result.ContractId);
        Assert.Null(result.Contract);
    }

    [Fact]
    public void ToModel_WithNullRevisionContainerDtos_ReturnsEmptyList()
    {
        // Arrange
        List<RevisionContainerDto>? revisionContainerDtos = null;

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
        var revisionContainerDtos = new List<RevisionContainerDto>
        {
            new RevisionContainerDto
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                RevisionContainerName = "Test Revision Container 1",
                RevisionNumber = 1,
                RevisionContainerDate = DateTime.UtcNow,
                TagData = new List<ITagDataDto>(),
                RevisionContainerReview = new RevisionContainerReviewDto(),
                ContractId = Guid.NewGuid(),
                Contract = null,
            },
            new RevisionContainerDto
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                RevisionContainerName = "Test Revision Container 2",
                RevisionNumber = 2,
                RevisionContainerDate = DateTime.UtcNow,
                TagData = new List<ITagDataDto>(),
                RevisionContainerReview = new RevisionContainerReviewDto(),
                ContractId = Guid.NewGuid(),
                Contract = null,
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
            Assert.Equal(revisionContainerDtos[i].CreatedDate, result[i].CreatedDate);
            Assert.Equal(revisionContainerDtos[i].ModifiedDate, result[i].ModifiedDate);
            Assert.Equal(revisionContainerDtos[i].RevisionContainerName, result[i].RevisionContainerName);
            Assert.Equal(revisionContainerDtos[i].RevisionNumber, result[i].RevisionNumber);
            Assert.Equal(revisionContainerDtos[i].RevisionContainerDate, result[i].RevisionContainerDate);
            Assert.NotNull(result[i].RevisionContainerReview);
            Assert.Equal(revisionContainerDtos[i].ContractId, result[i].ContractId);
            Assert.Null(result[i].Contract);
        }
    }
}
