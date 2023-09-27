using datasheetapi.Exceptions;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace tests.Services;
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
    public async Task GetContract_ThrowsNotFoundException_WhenInvalidId()
    {
        // Arrange
        var contractId = Guid.NewGuid();
        _contractRepositoryMock.Setup(x => x.GetContract(contractId)).ReturnsAsync((Contract?)null);

        // Act
        await Assert.ThrowsAsync<NotFoundException>(() => _contractService.GetContract(contractId));

        // Assert
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
