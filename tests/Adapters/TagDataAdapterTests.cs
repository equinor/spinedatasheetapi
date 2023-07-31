using datasheetapi.Adapters;
using datasheetapi.Dtos;
using datasheetapi.Models;

namespace tests.Adapters;

public class TagDataAdapterTests
{
    [Fact]
    public void ToDtoOrNull_WithNullTagData_ReturnsNull()
    {
        // Arrange
        ITagData? tagData = null;

        // Act
        var result = tagData.ToDtoOrNull();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToDtoOrNull_WithNonNullTagData_ReturnsTagDataDto()
    {
        // Arrange
        var tagData = new InstrumentTagData
        {
            Id = Guid.NewGuid(),
            TagNo = "1234",
            Area = "Area 1",
            Category = "Instrument",
            Description = "Instrument Tag",
            Discipline = "Instrumentation",
            Version = 1,
            InstrumentPurchaserRequirement = new InstrumentPurchaserRequirement(),
            InstrumentSupplierOfferedProduct = new InstrumentSupplierOfferedProduct(),
        };

        // Act
        var result = tagData.ToDtoOrNull();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tagData.Id, result.Id);
        Assert.Equal(tagData.TagNo, result.TagNo);
        Assert.Equal(tagData.Area, result.Area);
        Assert.Equal(tagData.Category, result.Category);
        Assert.Equal(tagData.Description, result.Description);
        Assert.Equal(tagData.Discipline, result.Discipline);
        Assert.Equal(tagData.Version, result.Version);
        Assert.Null(result.RevisionContainer);
        Assert.IsType<InstrumentTagDataDto>(result);
        var instrumentDto = (InstrumentTagDataDto)result;
        Assert.Equal(tagData.InstrumentPurchaserRequirement, instrumentDto.InstrumentPurchaserRequirement);
        Assert.Equal(tagData.InstrumentSupplierOfferedProduct, instrumentDto.InstrumentSupplierOfferedProduct);
    }

    [Fact]
    public void ToDto_WithNullTagData_ReturnsEmptyList()
    {
        // Arrange
        List<ITagData>? tagData = null;

        // Act
        var result = tagData.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToDto_WithNonNullTagData_ReturnsListOfTagDataDtos()
    {
        // Arrange
        var tagData = new List<ITagData>
        {
            new InstrumentTagData
            {
                Id = Guid.NewGuid(),
                TagNo = "1234",
                Area = "Area 1",
                Category = "Instrument",
                Description = "Instrument Tag",
                Discipline = "Instrumentation",
                Version = 1,
                InstrumentPurchaserRequirement = new InstrumentPurchaserRequirement(),
                InstrumentSupplierOfferedProduct = new InstrumentSupplierOfferedProduct(),
            },
            new ElectricalTagData
            {
                Id = Guid.NewGuid(),
                TagNo = "5678",
                Area = "Area 2",
                Category = "Electrical",
                Description = "Electrical Tag",
                Discipline = "Electrical",
                Version = 2,
                ElectricalPurchaserRequirement = new ElectricalPurchaserRequirement(),
                ElectricalSupplierOfferedProduct = new ElectricalSupplierOfferedProduct(),
            },
        };

        // Act
        var result = tagData.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tagData.Count, result.Count);
        for (int i = 0; i < tagData.Count; i++)
        {
            Assert.Equal(tagData[i].Id, result[i].Id);
            Assert.Equal(tagData[i].TagNo, result[i].TagNo);
            Assert.Equal(tagData[i].Area, result[i].Area);
            Assert.Equal(tagData[i].Category, result[i].Category);
            Assert.Equal(tagData[i].Description, result[i].Description);
            Assert.Equal(tagData[i].Discipline, result[i].Discipline);
            Assert.Equal(tagData[i].Version, result[i].Version);
            Assert.Null(result[i].RevisionContainer);
            if (tagData[i] is InstrumentTagData instrumentTagData)
            {
                Assert.IsType<InstrumentTagDataDto>(result[i]);
                var instrumentDto = (InstrumentTagDataDto)result[i];
                Assert.Equal(instrumentTagData.InstrumentPurchaserRequirement, instrumentDto.InstrumentPurchaserRequirement);
                Assert.Equal(instrumentTagData.InstrumentSupplierOfferedProduct, instrumentDto.InstrumentSupplierOfferedProduct);
            }
            else if (tagData[i] is ElectricalTagData electricalTagData)
            {
                Assert.IsType<ElectricalTagDataDto>(result[i]);
                var electricalDto = (ElectricalTagDataDto)result[i];
                Assert.Equal(electricalTagData.ElectricalPurchaserRequirement, electricalDto.ElectricalPurchaserRequirement);
                Assert.Equal(electricalTagData.ElectricalSupplierOfferedProduct, electricalDto.ElectricalSupplierOfferedProduct);
            }
            else if (tagData[i] is MechanicalTagData mechanicalTagData)
            {
                Assert.IsType<MechanicalTagDataDto>(result[i]);
                var mechanicalDto = (MechanicalTagDataDto)result[i];
                Assert.Equal(mechanicalTagData.MechanicalPurchaserRequirement, mechanicalDto.MechanicalPurchaserRequirement);
                Assert.Equal(mechanicalTagData.MechanicalSupplierOfferedProduct, mechanicalDto.MechanicalSupplierOfferedProduct);
            }
            else
            {
                Assert.IsType<TagDataDto>(result[i]);
            }
        }
    }
}
