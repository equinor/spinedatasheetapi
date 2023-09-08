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

    public async Task<List<FusionPersonResultV1>> GetAllPersonsOnProject(string fusionContextId, string search, int top, int skip)
    {
        var fusionSearchObject = new FusionSearchObject
        {
            // Filter = $"positions/any(p: p/isActive eq true and p/project/id eq '{fusionContextId}' and p/contract eq null)",
            Top = top,
            Skip = skip
        };

        if (!string.IsNullOrEmpty(search))
        {
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


    private async Task<FusionPerson?> GetPersonAsync(string azureId)
    {
        if (_cache.TryGetValue<FusionPerson>($"FusionPerson_{azureId}", out var cachedPerson))
            return cachedPerson;

        var result = await _downstreamApi.CallApiForUserAsync(
            "FusionPeople", options =>
            {
                options.HttpMethod = HttpMethod.Get;
                options.RelativePath = $"persons/{azureId}?api-version=3.0&$expand=contracts,positions";
            });

        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadAsStringAsync();
            _logger.LogWarning("Failed to retrieve person from Fusion. AzureId: {AzureId}, Error: {Error}", azureId,
                error);
            return null;
        }

        var person = await result.Content.ReadFromJsonAsync<FusionPerson>();
        ArgumentNullException.ThrowIfNull(person);
        _cache.Set($"FusionPerson_{azureId}", person, TimeSpan.FromMinutes(15));
        return person;
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