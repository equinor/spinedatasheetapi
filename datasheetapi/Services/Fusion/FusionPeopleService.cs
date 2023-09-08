using datasheetapi.Models.Fusion;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Abstractions;

namespace datasheetapi.Services;

public class FusionPeopleService
{
    private readonly IDownstreamApi _downstreamApi;
    private readonly IMemoryCache _cache;
    private readonly ILogger<FusionPeopleService> _logger;

    public FusionPeopleService(
        IDownstreamApi downstreamApi,
        IMemoryCache cache,
        ILogger<FusionPeopleService> logger)
    {
        _downstreamApi = downstreamApi;
        _cache = cache;
        _logger = logger;
    }

    public async Task<List<FusionPersonResultV1>> GetAllPersonsOnProject(string orgChartId, string search, int top, int skip)
    {
        Console.WriteLine("FusearchContextId: " + orgChartId);
        Console.WriteLine("Search: " + search);
        var fusionSearchObject = new FusionSearchObject
        {
            // Filter = $"positions/any(p: p/isActive eq true and p/project/id eq '${orgChartId}' and p/contract eq null)",
            // Filter = $"positions/any(p: p/isActive eq true and p/contract eq null)",
            Top = top,
            Skip = skip
        };

        if (!string.IsNullOrEmpty(search))
        {
            // fusionSearchObject.Filter += $" and {AddSearch(search)}";
            fusionSearchObject.Filter += $"{AddSearch(search)}";
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