namespace datasheetapi.Models.Fusion;

public record FusionPersonResponseV1(List<FusionPersonResultV1> Results, int Count);

public record FusionPersonResultV1(FusionPersonV1 Document);

public record FusionPersonV1(
    string AzureUniqueId,
    string Mail,
    string Name,
    string JobTitle,
    string Department,
    string FullDepartment,
    string MobilePhone,
    string OfficeLocation,
    string Upn,
    string PreferredContactMail,
    string AccountType,
    string Company,
    IReadOnlyList<ContractV1> Contracts,
    IReadOnlyList<PositionV1> Positions,
    string AccountClassification,
    string ManagerAzureUniqueId
);
public record BasePositionV1(
    string Id,
    string Name,
    string Type,
    object Discipline
);

public record ContractV1(
    string Id,
    string Name,
    string Number
);

public record PositionV1(
    string PositionId,
    string PositionExternalId,
    string Id,
    string ParentPositionId,
    IReadOnlyList<object> TaskOwnerIds,
    string Name,
    string Obs,
    BasePositionV1 BasePosition,
    ProjectV1 Project,
    ContractV1 Contract,
    bool IsTaskOwner,
    DateTime AppliesFrom,
    DateTime AppliesTo,
    double Workload
);

public record ProjectV1(
    string Id,
    string Name,
    string DomainId,
    string Type
);

public record FusionProjectMasterV1(
    string Id,
    string Name
);
