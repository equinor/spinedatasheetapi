namespace datasheetapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fusion.Integration;

using Microsoft.Extensions.Logging;

using Models;

using Newtonsoft.Json;

public class FusionService : IFusionService
{
    private readonly IFusionContextResolver _fusionContextResolver;
    private readonly ILogger<FusionService> _logger;
    private readonly IFusionProfileResolver _profileResolver;
    private readonly IAzureUserCacheService _azureUserCacheService;


    public FusionService(
        IFusionContextResolver fusionContextResolver,
        IFusionProfileResolver profileResolver,
        IAzureUserCacheService azureUserCacheService,
        ILogger<FusionService> logger)
    {
        _fusionContextResolver = fusionContextResolver;
        _profileResolver = profileResolver;
        _azureUserCacheService = azureUserCacheService;
        _logger = logger;
    }

    public async Task<FusionProjectMaster> ProjectMasterAsync(Guid contextId)
    {
        var projectMasterContext = await ResolveProjectMasterContext(contextId);

        // ?: Did we obtain a ProjectMaster context?
        if (projectMasterContext == null)
        {
            // -> No, still not found. Then we log this and fail hard, as the callee should have provided with a
            // valid ProjectMaster (context) ID.
            Console.WriteLine(
                "Could not resolve ProjectMaster context from Fusion using GUID '{ProjectMasterId}'" +
                contextId);
            throw new Exception();
        }

        var serializedProjectMaster = JsonConvert.SerializeObject(projectMasterContext.Value);
        FusionProjectMaster? fusionProjectMaster = JsonConvert.DeserializeObject<FusionProjectMaster>(serializedProjectMaster);

        if (fusionProjectMaster == null)
        {
            Console.WriteLine(
                "Project Master with ID '{ProjectMasterId}' was obtained from Fusion, but conversion to explicit " +
                "type failed" +
                contextId);
            throw new Exception();
        }

        return fusionProjectMaster;
    }

    private async Task<FusionContext?> ResolveProjectMasterContext(Guid contextId)
    {
        FusionContext? projectMasterContext = await _fusionContextResolver.ResolveContextAsync(contextId, FusionContextType.ProjectMaster);

        Console.WriteLine("ResolveProjectMasterContext - contextId: " + contextId);
        Console.WriteLine("ResolveProjectMasterContext - projectMasterContext: " + projectMasterContext);
        // It might be the GUID provided was the ProjectMaster ID and not the GUID of the Fusion Context. Will
        // thus attempt to query for the ProjectMaster "directly" if not found.
        if (projectMasterContext == null)
        {
            IEnumerable<FusionContext> queryContextsAsync = await _fusionContextResolver.QueryContextsAsync(
                query =>
                {
                    query
                        .WhereTypeIn(FusionContextType.ProjectMaster)
                        .WhereExternalId(contextId.ToString(), QueryOperator.Equals);
                });
            projectMasterContext = queryContextsAsync.FirstOrDefault();
        }

        return projectMasterContext;
    }

    public async Task<Fusion.Integration.Profile.FusionPersonProfile?> ResolveUserFromPersonId(Guid azureUniqueId)
    {
        var fusionPersonProfile = await _profileResolver.ResolvePersonBasicProfileAsync(new Fusion.Integration.Profile.PersonIdentifier(azureUniqueId));
        return fusionPersonProfile;
    }

    private int AddUserToCache(Fusion.Integration.Profile.FusionPersonProfile fusionPersonProfile)
    {
        throw new NotImplementedException();
    }
}
