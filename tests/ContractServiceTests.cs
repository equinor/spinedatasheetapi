using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace tests;
public class ContractServiceTests
{
    private readonly Mock<IContractRepository> _contractRepositoryMock;
    private readonly ContractService _contractService;

    public ContractServiceTests()
    {
        _contractRepositoryMock = new Mock<IContractRepository>();
        _contractService = new ContractService(Mock.Of<ILoggerFactory>(), _contractRepositoryMock.Object);
    }

    [Fact]
    public async Task GetContract_ReturnsContract_WhenValidId()
    {
        // Arrange
        var contractId = Guid.NewGuid();
        var contract = new Contract { Id = contractId };
        _contractRepositoryMock.Setup(x => x.GetContract(contractId)).ReturnsAsync(contract);

        // Act
        var result = await _contractService.GetContract(contractId);

        // Assert
        Assert.Equal(contract, result);
        _contractRepositoryMock.Verify(x => x.GetContract(contractId), Times.Once);
    }

    [Fact]
    public async Task GetContract_ReturnsNull_WhenInvalidId()
    {
        // Arrange
        var contractId = Guid.NewGuid();
        _contractRepositoryMock.Setup(x => x.GetContract(contractId)).ReturnsAsync((Contract)null);

        // Act
        var result = await _contractService.GetContract(contractId);

        // Assert
        Assert.Null(result);
        _contractRepositoryMock.Verify(x => x.GetContract(contractId), Times.Once);
    }

    [Fact]
    public async Task GetContracts_ReturnsListOfContracts()
    {
        // Arrange
        var contracts = new List<Contract> { new Contract { Id = Guid.NewGuid() }, new Contract { Id = Guid.NewGuid() } };
        _contractRepositoryMock.Setup(x => x.GetContracts()).ReturnsAsync(contracts);

        // Act
        var result = await _contractService.GetContracts();

        // Assert
        Assert.Equal(contracts, result);
        _contractRepositoryMock.Verify(x => x.GetContracts(), Times.Once);
    }

    [Fact]
    public async Task GetContractsForContractor_ReturnsListOfContracts_WhenValidContractorId()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var contracts = new List<Contract> { new Contract { Id = Guid.NewGuid(), ContractorId = contractorId }, new Contract { Id = Guid.NewGuid(), ContractorId = contractorId } };
        _contractRepositoryMock.Setup(x => x.GetContractForContractor(contractorId)).ReturnsAsync(contracts);

        // Act
        var result = await _contractService.GetContractsForContractor(contractorId);

        // Assert
        Assert.Equal(contracts, result);
        _contractRepositoryMock.Verify(x => x.GetContractForContractor(contractorId), Times.Once);
    }

    [Fact]
    public async Task GetContractsForContractor_ReturnsEmptyList_WhenInvalidContractorId()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        _contractRepositoryMock.Setup(x => x.GetContractForContractor(contractorId)).ReturnsAsync(new List<Contract>());

        // Act
        var result = await _contractService.GetContractsForContractor(contractorId);

        // Assert
        Assert.Empty(result);
        _contractRepositoryMock.Verify(x => x.GetContractForContractor(contractorId), Times.Once);
    }

    [Fact]
    public async Task GetContractsForProject_ReturnsListOfContracts_WhenValidProjectId()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var contracts = new List<Contract> { new Contract { Id = Guid.NewGuid(), ProjectId = projectId }, new Contract { Id = Guid.NewGuid(), ProjectId = projectId } };
        _contractRepositoryMock.Setup(x => x.GetContractForProject(projectId)).ReturnsAsync(contracts);

        // Act
        var result = await _contractService.GetContractsForProject(projectId);

        // Assert
        Assert.Equal(contracts, result);
        _contractRepositoryMock.Verify(x => x.GetContractForProject(projectId), Times.Once);
    }

    [Fact]
    public async Task GetContractsForProject_ReturnsEmptyList_WhenInvalidProjectId()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        _contractRepositoryMock.Setup(x => x.GetContractForProject(projectId)).ReturnsAsync(new List<Contract>());

        // Act
        var result = await _contractService.GetContractsForProject(projectId);

        // Assert
        Assert.Empty(result);
        _contractRepositoryMock.Verify(x => x.GetContractForProject(projectId), Times.Once);
    }
}