using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using datasheetapi.Adapters;
using datasheetapi.Dtos;
using datasheetapi.Models;
using datasheetapi.Services;

using Moq;

using Xunit;

namespace tests
{
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
        public async Task GetAllTagDataDtos_ReturnsExpectedResult()
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
            var result = await tagDataService.GetAllTagDataDtos();

            // Assert
            Assert.Equal(expectedTagData.ToDto(), result);
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
        public async Task GetTagDataDtoById_ReturnsNull_WhenFAMServiceReturnsNull()
        {
            // Arrange
            _mockFAMService.Setup(x => x.GetTagData(It.IsAny<Guid>())).ReturnsAsync(null as ITagData);

            // Act
            var result = await _tagDataService.GetTagDataDtoById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTagDataDtoById_ReturnsDto_WhenFAMServiceReturnsData()
        {
            // Arrange
            var tagData = new TagData { Id = Guid.NewGuid(), Description = "Test Tag" };
            _mockFAMService.Setup(x => x.GetTagData(tagData.Id)).ReturnsAsync(tagData);

            // Act
            var result = await _tagDataService.GetTagDataDtoById(tagData.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tagData.Id, result?.Id);
            Assert.Equal(tagData.Description, result?.Description);
        }

        [Fact]
        public async Task GetTagDataById_ReturnsNull_WhenFAMServiceReturnsNull()
        {
            // Arrange
            _mockFAMService.Setup(x => x.GetTagData(It.IsAny<Guid>())).ReturnsAsync(null as ITagData);

            // Act
            var result = await _tagDataService.GetTagDataById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTagDataById_ReturnsData_WhenFAMServiceReturnsData()
        {
            // Arrange
            var tagData = new TagData { Id = Guid.NewGuid(), Description = "Test Tag" };
            _mockFAMService.Setup(x => x.GetTagData(tagData.Id)).ReturnsAsync(tagData);

            // Act
            var result = await _tagDataService.GetTagDataById(tagData.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tagData.Id, result?.Id);
            Assert.Equal(tagData.Description, result?.Description);
        }

        [Fact]
        public async Task GetTagDataDtosForProject_ReturnsEmptyList_WhenFAMServiceReturnsEmptyList()
        {
            // Arrange
            _mockFAMService.Setup(x => x.GetTagDataForProject(It.IsAny<Guid>())).ReturnsAsync(new List<ITagData>());

            // Act
            var result = await _tagDataService.GetTagDataDtosForProject(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTagDataDtosForProject_ReturnsList_WhenFAMServiceReturnsData()
        {
            // Arrange
            var tagDataList = new List<TagData>
        {
            new TagData { Id = Guid.NewGuid(), Description = "Test Tag 1" },
            new TagData { Id = Guid.NewGuid(), Description = "Test Tag 2" },
            new TagData { Id = Guid.NewGuid(), Description = "Test Tag 3" }
        };
            _mockFAMService.Setup(x => x.GetTagDataForProject(It.IsAny<Guid>())).ReturnsAsync(tagDataList.Cast<ITagData>().ToList());

            // Act
            var result = await _tagDataService.GetTagDataDtosForProject(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tagDataList.Count, result.Count);
            for (int i = 0; i < tagDataList.Count; i++)
            {
                Assert.Equal(tagDataList[i].Id, result[i].Id);
                Assert.Equal(tagDataList[i].Description, result[i].Description);
            }
        }
    }
}
