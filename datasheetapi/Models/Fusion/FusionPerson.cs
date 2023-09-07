using JetBrains.Annotations;

namespace SpineReviewApi.Services.Fusion.Models.FusionPersons;

public record FusionPersonResponse(List<FusionPerson> Persons, int Count);

public record FusionPerson(
    string AzureUniqueId,
    string Mail,
    string Name,
    string Upn,
    string AccountType,
    IReadOnlyList<Contract> Contracts,
    IReadOnlyList<Position> Positions,
    string AccountClassification
);
public record Contract(string ContractNumber, Project Project);

public record Position(Project Project, Contract? Contract);

public record Project(Guid Id, string Name);


