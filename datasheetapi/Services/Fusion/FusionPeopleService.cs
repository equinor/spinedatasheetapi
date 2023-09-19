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
        // var context = await _fusionContextResolver.ResolveContextAsync(projectMasterId);
        // if (context == null)
        // {
        //     throw new Exception($"Could not resolve context for project {projectMasterId}");
        // }
        // var contextRelations = await _fusionContextResolver.GetContextRelationsAsync(Guid.Parse(projectMasterId));

        string orgChartId = string.Empty;

        // foreach (var contextRelation in contextRelations)
        // {
        //     Console.WriteLine("ContextRelationType: " + contextRelation.Type);
        //     Console.WriteLine("ContextRelationId: " + contextRelation.Id);
        //     Console.WriteLine("ContextRelationTitle: " + contextRelation.Title);
        //     Console.WriteLine("ContextRelationExternalId: " + contextRelation.ExternalId);


        //     if (contextRelation.Type == FusionContextType.OrgChart)
        //     {
        //         orgChartId = contextRelation.Id.ToString();
        //         Console.WriteLine("OrgChartId: " + orgChartId);
        //     }
        // }

        // Console.WriteLine("Context: " + context);

        Console.WriteLine("FusearchContextId: " + projectMasterId);
        Console.WriteLine("Search: " + search);
        var fusionSearchObject = new FusionSearchObject
        {
            Filter = $"positions/any(p: p/isActive eq true and p/project/id eq 'c8bf14bc-d649-4d57-90d9-e4a0f78298b4' and p/contract eq null)",
            // Filter = $"positions/any(p: p/project/id eq '${projectMasterId}')",
            // Filter = $"positions/any(p: p/isActive eq true and p/project/id eq '${projectMasterId}' and p/contract eq null)",
            // Filter = $"positions/any(p: p/isActive eq true and p/contract eq null)",
            Top = 100,
            Skip = skip
        };

        if (!string.IsNullOrEmpty(search))
        {
            fusionSearchObject.Filter += $" and {AddSearch(search)}";
            // fusionSearchObject.Filter += $"{AddSearch(search)}";
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