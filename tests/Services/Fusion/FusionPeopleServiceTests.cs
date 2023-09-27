using datasheetapi.Models.Fusion;
using datasheetapi.Services;

using Fusion.Integration;

using Microsoft.Identity.Abstractions;

using Moq;

namespace datasheetapi.tests.Services
{
    public class FusionPeopleServiceTests
    {
        private readonly Mock<IDownstreamApi> _downstreamApiMock;
        private readonly Mock<IFusionContextResolver> _fusionContextResolverMock;
        private readonly FusionPeopleService _fusionPeopleService;

        public FusionPeopleServiceTests()
        {
            _downstreamApiMock = new Mock<IDownstreamApi>();
            _fusionContextResolverMock = new Mock<IFusionContextResolver>();
            _fusionPeopleService = new FusionPeopleService(_downstreamApiMock.Object, _fusionContextResolverMock.Object);
        }

        [Fact]
        public async Task GetAllPersonsOnProject_ReturnsEmptyList_WhenOrgChartIdIsNull()
        {
            // Arrange
            var fusionContextId = Guid.NewGuid();
            var search = "test";
            var top = 10;
            var skip = 0;

            _fusionContextResolverMock.Setup(x => x.GetContextRelationsAsync(fusionContextId, null))
                .ReturnsAsync(new List<FusionContext>());

            // Act
            var result = await _fusionPeopleService.GetAllPersonsOnProject(fusionContextId, search, top, skip);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllPersonsOnProject_ReturnsListOfPersons_WhenOrgChartIdIsNotNull()
        {
            // Arrange
            var fusionContextId = Guid.NewGuid();
            var search = "test";
            var top = 20;
            var skip = 0;
            var orgChartId = Guid.NewGuid().ToString();

            var contextRelations = new List<FusionContext>
            {
                new() {
                    Type = FusionContextType.OrgChart,
                    ExternalId = orgChartId
                }
            };

            _fusionContextResolverMock.Setup(x => x.GetContextRelationsAsync(fusionContextId, null))
                .ReturnsAsync(contextRelations);

            _downstreamApiMock.Setup(x => x.PostForUserAsync<FusionSearchObject, FusionPersonResponseV1>(
                    "FusionPeople", It.IsAny<FusionSearchObject>(),
                    It.IsAny<Action<DownstreamApiOptionsReadOnlyHttpMethod>>(), null, default))
                .ReturnsAsync(new FusionPersonResponseV1
                (
                    Results: new List<FusionPersonResultV1>
                    {
                        new (
                            Document: new FusionPersonV1(
                                AzureUniqueId: Guid.NewGuid().ToString(),
                                Name: "Test User",
                                Mail: "test.user@test.com",
                                AccountType: "User",
                                AccountClassification: "Internal"
                            )
                        )
                    },
                    Count: 1
                ));

            // Act
            var result = await _fusionPeopleService.GetAllPersonsOnProject(fusionContextId, search, top, skip);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsType<List<FusionPersonV1>>(result);
        }
    }
}
