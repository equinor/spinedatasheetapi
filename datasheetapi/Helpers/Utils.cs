using System.Security.Claims;

namespace datasheetapi.Helpers;

public static class Utils
{
    public static Guid GetAzureUniqueId(ClaimsPrincipal user)
    {
        var fusionIdentity = user.Identities.FirstOrDefault(i =>
            i is Fusion.Integration.Authentication.FusionIdentity)
                as Fusion.Integration.Authentication.FusionIdentity;
        var azureUniqueId = fusionIdentity?.Profile?.AzureUniqueId ??
            throw new Exception("Could not get Azure Unique Id");
        return azureUniqueId;
    }
}
