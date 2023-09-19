namespace datasheetapi.Services;

public interface IUserTagService
{
    Task<List<UserTagDto>> GetUsersFromOrgChart(string fusionContextId, string search, int top, int skip);
}

public class UserTagService : IUserTagService
{
    private readonly IFusionPeopleService _fusionPeopleService;


    public UserTagService(IFusionPeopleService fusionPeopleService)
    {
        _fusionPeopleService = fusionPeopleService;
    }

    public async Task<List<UserTagDto>> GetUsersFromOrgChart(string fusionContextId, string search, int top, int skip)
    {
        var fusionRepsonse = await _fusionPeopleService.GetAllPersonsOnProject(fusionContextId, search ?? "", top, skip);

        var userTagDtos = fusionRepsonse.Select(fusionPersonResultV1 => new UserTagDto
        {
            AzureUniqueId = fusionPersonResultV1.AzureUniqueId,
            DisplayName = fusionPersonResultV1.Name,
            Mail = fusionPersonResultV1.Mail,
            AccountType = fusionPersonResultV1.AccountType
        }).ToList();

        return userTagDtos;
    }
}


