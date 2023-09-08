using datasheetapi.Adapters;

namespace datasheetapi.Services;

public class TagUserService
{
    private readonly IFAMService _FAMService;

    public TagUserService(IFAMService FAMService)
    {
        _FAMService = FAMService;
    }

    // public async Task GetFusionUserResponsibilitesOnProject(string contractId, string search, int top, int skip)
    // {
    //     var response = await _fusionPeopleService.GetAllPersonsOnContract(contractId, project.FusionId, search, top, skip);

    //     var commentResponsibleMails =
    //         (await _commentResponsibilityRepository.GetCommentResponsiblesByContract(contractId))
    //         .Select(usr => usr.User!.Upn).ToList();

    //     var responsibleUsers = response
    //         .Persons
    //         .Select(person => new ResponsibleUser
    //         {
    //             Id = person.AzureUniqueId,
    //             DisplayName = person.Name,
    //             Mail = person.Mail,
    //             Responsibilities = GetRoleFromPerson(person, project, contractId, commentResponsibleMails)
    //         })
    //         .ToList();

    //     return new ResponsibleUserResult
    //     {
    //         ResponsibleUsers = responsibleUsers,
    //         Count = response.Count
    //     };
    // }
}
