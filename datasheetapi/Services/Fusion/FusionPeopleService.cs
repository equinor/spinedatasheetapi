using Mapster;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Abstractions;
using SpineReviewApi.Db.Repositories.ProjectRepository;
using SpineReviewApi.Services.Constants;
using SpineReviewApi.Services.Fusion.Models.FusionPersons;
using System.Net.Http.Json;
using JetBrains.Annotations;

namespace SpineReviewApi.Services.Fusion;

public class FusionPeopleService : IFusionPeopleService
{
    private readonly IDownstreamApi _downstreamApi;
    private readonly IMemoryCache _cache;
    private readonly ILogger<FusionPeopleService> _logger;
    private readonly IProjectRepository _projectRepository;

    public FusionPeopleService(
        IDownstreamApi downstreamApi,
        IMemoryCache cache,
        ILogger<FusionPeopleService> logger,
        IProjectRepository projectRepository)
    {
        _downstreamApi = downstreamApi;
        _cache = cache;
        _logger = logger;
        _projectRepository = projectRepository;
    }

    /// <summary>
    /// Verify that a user is a part of a contract, or the project itself.
    /// </summary>
    public async Task<bool> IsPartOfProject(Guid projectGuid, string azureid)
    {
        var person = await GetPersonAsync(azureid);
        if (person is null) return false;

        var partOfProject = person.Positions.Any(p => p.Project.Id == projectGuid);
        var partOfContract = person.Contracts.Any(c => c.Project.Id == projectGuid);

        return partOfProject || partOfContract;
    }

    /// <summary>
    /// Verify that a user has a position in a project.
    /// </summary>
    public async Task<bool> IsEmployedInProjectAsync(string projectId, string azureId)
    {
        var person = await GetPersonAsync(azureId);
        if (person is null) return false;

        var fusionGuid = await _projectRepository.GetFusionGuid(projectId);

        if (fusionGuid is null)
        {
            _logger.LogInformation("Cannot verify access to project as it does not exist. ProjectId: {ProjectId}",
                projectId);
            return false;
        }

        return IsEmployedInProject(person, (Guid)fusionGuid);
    }

    public bool IsEmployedInProject(FusionPerson person, Guid fusionGuid)
    {
        return person.Positions.Any(position => position.Project.Id == fusionGuid) &&
               person is { AccountClassification: "Internal", AccountType: "Employee" };
    }


    /// <summary>
    /// Gets all persons on a given project.
    /// Filters out all contractors (i.e., where p/contract is null).
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="search">Search term.</param>
    /// <param name="top">Number of results to retrieve.</param>
    /// <param name="skip">Number of results to skip.</param>
    /// <returns>A list of FusionPersons.</returns>
    public async Task<FusionPersonResponse> GetAllPersonsOnProject(string projectId, string search, int top, int skip)
    {
        var project = await _projectRepository.GetByProjectId(projectId);
        ArgumentNullException.ThrowIfNull(project);

        var fusionSearchObject = new FusionSearchObject
        {
            Filter = $"positions/any(p: p/isActive eq true and p/project/id eq '{project.FusionId}' and p/contract eq null)",
            Top = top,
            Skip = skip
        };

        if (!string.IsNullOrEmpty(search))
        {
            fusionSearchObject.Filter += $" and {AddSearch(search)}";
        }

        return await QueryFusionPeopleService(fusionSearchObject);
    }

    public async Task<FusionPersonResponse> GetByUpns(string projectId, List<string> upns)
    {
        if (!upns.Any()) return new FusionPersonResponse(new List<FusionPerson>(), 0);

        var project = await _projectRepository.GetByProjectId(projectId);
        ArgumentNullException.ThrowIfNull(project);

        var upnsWithFilter = upns.Select((upn, index) => $"upn eq '{upn}' {(index == upns.Count - 1 ? "" : "or")}");
        var asFilter = string.Join(" ", upnsWithFilter);

        var fusionSearchObject = new FusionSearchObject
        { Filter = $"positions/any(p: p/project/id eq '{project.FusionId}') and {asFilter}" };

        return await QueryFusionPeopleService(fusionSearchObject);
    }

    public async Task<FusionPersonResponse> QueryFusionPeopleService(FusionSearchObject fusionSearchObject)
    {
        var response = await _downstreamApi.PostForUserAsync<FusionSearchObject, FusionPersonResponseV1>(
            ApiKeys.FusionPeople, fusionSearchObject,
            opt => opt.RelativePath = "search/persons/query?api-version=1.0");

        return response?.Results is null
            ? new FusionPersonResponse(new List<FusionPerson>(), 0)
            : new FusionPersonResponse(
                response.Results.Select(result => result.Document.Adapt<FusionPerson>()).ToList(),
                response.Count
            );
    }

    private async Task<FusionPerson?> GetPersonAsync(string azureId)
    {
        if (_cache.TryGetValue<FusionPerson>($"FusionPerson_{azureId}", out var cachedPerson))
            return cachedPerson;

        var result = await _downstreamApi.CallApiForUserAsync(
            ApiKeys.FusionPeople, options =>
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

[PublicAPI]
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