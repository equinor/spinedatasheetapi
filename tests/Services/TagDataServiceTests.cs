using datasheetapi.Exceptions;
using datasheetapi.Models;
using datasheetapi.Services;

using Moq;

using Xunit;

namespace tests.Services;

public class TagDataServiceTests
{
    private readonly Mock<IFAMService> _mockFAMService;
    private readonly TagDataService _tagDataService;

    public TagDataServiceTests()
    {
        _mockFAMService = new Mock<IFAMService>();
        _tagDataService = new TagDataService(_mockFAMService.Object);
    }

    [Fact]
    public async Task GetAllTagData_ReturnsExpectedResult()
    {
        // Arrange
        var expectedTagData = new List<ITagData>
            {
                new InstrumentTagData { Id = Guid.NewGuid(), Description = "Instrument 1" },
                new ElectricalTagData { Id = Guid.NewGuid(), Description = "Electrical 1" },
                new MechanicalTagData { Id = Guid.NewGuid(), Description = "Mechanical 1" }
            };

        _mockFAMService.Setup(x => x.GetTagData()).ReturnsAsync(expectedTagData);

        var tagDataService = new TagDataService(_mockFAMService.Object);

        // Act
        var result = await tagDataService.GetAllTagData();

        // Assert
        Assert.Equal(expectedTagData, result);
    }

    [Fact]
    public async Task GetAllTagData_ReturnsEmptyList_WhenFAMServiceReturnsEmptyList()
    {
        // Arrange
        _mockFAMService.Setup(x => x.GetTagData()).ReturnsAsync(new List<ITagData>());

        // Act
        var result = await _tagDataService.GetAllTagData();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllTagData_ReturnsList_WhenFAMServiceReturnsData()
    {
        // Arrange
        var tagDataList = new List<TagData>
        {
            new TagData { Id = Guid.NewGuid(), Description = "Test Tag 1" },
            new TagData { Id = Guid.NewGuid(), Description = "Test Tag 2" },
            new TagData { Id = Guid.NewGuid(), Description = "Test Tag 3" }
        };
        _mockFAMService.Setup(x => x.GetTagData()).ReturnsAsync(tagDataList.Cast<ITagData>().ToList());

        // Act
        var result = await _tagDataService.GetAllTagData();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tagDataList.Count, result.Count);
        for (int i = 0; i < tagDataList.Count; i++)
        {
            Assert.Equal(tagDataList[i].Id, result[i].Id);
            Assert.Equal(tagDataList[i].Description, result[i].Description);
        }
    }

    [Fact]
    public async Task GetTagDataById_ThrowsException_WhenFAMServiceReturnsNull()
    {
        // Arrange
        _mockFAMService.Setup(x => x.GetTagData(It.IsAny<string>())).ReturnsAsync(null as ITagData);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _tagDataService.GetTagDataByTagNo("TAG-015"));
    }

    [Fact]
    public async Task GetTagDataById_ReturnsData_WhenFAMServiceReturnsData()
    {
        // Arrange
        var tagData = new TagData { TagNo = "TAG-016", Description = "Test Tag" };
        _mockFAMService.Setup(x => x.GetTagData(tagData.TagNo)).ReturnsAsync(tagData);

        // Act
        var result = await _tagDataService.GetTagDataByTagNo(tagData.TagNo);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tagData.TagNo, result?.TagNo);
        Assert.Equal(tagData.Description, result?.Description);
    }
}
