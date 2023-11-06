using datasheetapi.Adapters;
using datasheetapi.Dtos;
using datasheetapi.Models;

namespace tests.Adapters;
public class ContractAdapterTests
{
    [Fact]
    public void ToDtoOrNull_WithNullContract_ReturnsNull()
    {
        // Arrange
        Contract? contract = null;

        // Act
        var result = contract.ToDtoOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToDtoOrNull_WithNonNullContract_ReturnsContractDto()
    {
        // Arrange
        var contract = new Contract
        {
            ContractName = "Test Contract",
            ProjectId = Guid.NewGuid(),
            Containers = new List<Container>(),
        };

        // Act
        var result = contract.ToDtoOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contract.ContractName, result.ContractName);
        Assert.Equal(contract.ProjectId, result.ProjectId);
        Assert.NotNull(result.RevisionContainers);
        Assert.Empty(result.RevisionContainers);
    }

    [Fact]
    public void ToDto_WithNullContracts_ReturnsEmptyList()
    {
        // Arrange
        List<Contract>? contracts = null;

        // Act
        var result = contracts.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToDto_WithNonNullContracts_ReturnsListOfContractDtos()
    {
        // Arrange
        var contracts = new List<Contract>
        {
            new Contract
            {
                ContractName = "Test Contract 1",
                ProjectId = Guid.NewGuid(),
                Containers = new List<Container>(),
            },
            new Contract
            {
                ContractName = "Test Contract 2",
                ProjectId = Guid.NewGuid(),
                Containers = new List<Container>(),
            },
        };

        // Act
        var result = contracts.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contracts.Count, result.Count);
        for (int i = 0; i < contracts.Count; i++)
        {
            Assert.Equal(contracts[i].ContractName, result[i].ContractName);
            Assert.Equal(contracts[i].ProjectId, result[i].ProjectId);
            Assert.NotNull(result[i].RevisionContainers);
            Assert.Empty(result[i].RevisionContainers);
        }
    }

    [Fact]
    public void ToModelOrNull_WithNullContractDto_ReturnsNull()
    {
        // Arrange
        ContractDto? contractDto = null;

        // Act
        var result = contractDto.ToModelOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToModelOrNull_WithNonNullContractDto_ReturnsContract()
    {
        // Arrange
        var contractDto = new ContractDto
        {
            ContractName = "Test Contract",
            ContractorId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            RevisionContainers = new List<ContainerDto>(),
        };

        // Act
        var result = contractDto.ToModelOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contractDto.ContractName, result.ContractName);
        Assert.Equal(contractDto.ProjectId, result.ProjectId);
        Assert.NotNull(result.Containers);
        Assert.Empty(result.Containers);
    }

    [Fact]
    public void ToModel_WithNullContractDtos_ReturnsEmptyList()
    {
        // Arrange
        List<ContractDto>? contractDtos = null;

        // Act
        var result = contractDtos.ToModel();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToModel_WithNonNullContractDtos_ReturnsListOfContracts()
    {
        // Arrange
        var contractDtos = new List<ContractDto>
        {
            new ContractDto
            {
                ContractName = "Test Contract 1",
                ContractorId = Guid.NewGuid(),
                ProjectId = Guid.NewGuid(),
                RevisionContainers = new List<ContainerDto>(),
            },
            new ContractDto
            {
                ContractName = "Test Contract 2",
                ContractorId = Guid.NewGuid(),
                ProjectId = Guid.NewGuid(),
                RevisionContainers = new List<ContainerDto>(),
            },
        };

        // Act
        var result = contractDtos.ToModel();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contractDtos.Count, result.Count);
        for (int i = 0; i < contractDtos.Count; i++)
        {
            Assert.Equal(contractDtos[i].ContractName, result[i].ContractName);
            Assert.Equal(contractDtos[i].ProjectId, result[i].ProjectId);
            Assert.NotNull(result[i].Containers);
            Assert.Empty(result[i].Containers);
        }
    }
}
