using datasheetapi.Models.Fusion;

using Fusion.Integration;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Abstractions;

namespace datasheetapi.Services;

public class FusionPeopleService
{
    private readonly IDownstreamApi _downstreamApi;
    private readonly IMemoryCache _cache;
    private readonly ILogger<FusionPeopleService> _logger;
    private readonly IFusionContextResolver _fusionContextResolver;


    public FusionPeopleService(
        IDownstreamApi downstreamApi,
        IMemoryCache cache,
        IFusionContextResolver fusionContextResolver,
        ILogger<FusionPeopleService> logger)
    {
        _downstreamApi = downstreamApi;
        _cache = cache;
        _fusionContextResolver = fusionContextResolver;
        _logger = logger;
    }

    public async Task<List<FusionPersonResultV1>> GetAllPersonsOnProject(string projectMasterId, string search, int top, int skip)
    {
        var contextRelations = await _fusionContextResolver.GetContextRelationsAsync(Guid.Parse(projectMasterId));

        string? orgChartId = string.Empty;

        foreach (var contextRelation in contextRelations)
        {
            if (contextRelation.Type == FusionContextType.OrgChart)
            {
                orgChartId = contextRelation.ExternalId?.ToString();
            }
        }

        if (string.IsNullOrEmpty(orgChartId))
        {
            return new List<FusionPersonResultV1>();
        }

        var fusionSearchObject = new FusionSearchObject
        {
            Filter = $"positions/any(p: p/isActive eq true and p/project/id eq '{orgChartId}' and p/contract eq null)",
            Top = 100,
            Skip = skip
        };

        if (!string.IsNullOrEmpty(search))
        {
            fusionSearchObject.Filter += $" and {AddSearch(search)}";
        }

        return await QueryFusionPeopleService(fusionSearchObject);
    }

    public async Task<List<FusionPersonResultV1>> QueryFusionPeopleService(FusionSearchObject fusionSearchObject)
    {
        var response = await _downstreamApi.PostForUserAsync<FusionSearchObject, FusionPersonResponseV1>(
            "FusionPeople", fusionSearchObject,
            opt => opt.RelativePath = "search/persons/query?api-version=1.0");

        return response?.Results ?? new List<FusionPersonResultV1>();
    }

    private static string AddSearch(string search)
    {
        return $"search.ismatch('{search}*', 'name,mail')";
    }
}

public class FusionSearchObject
{
    public string Filter { get; set; } = string.Empty;
    public string[] OrderBy { get; set; } = Array.Empty<string>();
    public int? Top { get; set; }
    public int? Skip { get; set; }
    public bool IncludeTotalResultCount { get; set; } = true;
    public bool MatchAll { get; set; } = true;
    public bool FullQueryMode { get; set; } = true;
}