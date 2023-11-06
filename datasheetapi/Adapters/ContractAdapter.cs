namespace datasheetapi.Adapters;
public static class ContractAdapter
{
    public static ContractDto? ToDtoOrNull(this Contract? contract)
    {
        if (contract is null) { return null; }
        return contract.ToDto();
    }

    private static ContractDto ToDto(this Contract contract)
    {
        return new ContractDto
        {
            Id = contract.Id,
            ContractName = contract.ContractName,
            ProjectId = contract.ProjectId,
            RevisionContainers = contract.Containers.ToDto(),
        };
    }

    public static List<ContractDto> ToDto(this List<Contract>? contracts)
    {
        if (contracts is null) { return new List<ContractDto>(); }
        return contracts.Select(ToDto).ToList();
    }

    public static Contract? ToModelOrNull(this ContractDto? contractDto)
    {
        if (contractDto is null) { return null; }
        return contractDto.ToModel();
    }

    private static Contract ToModel(this ContractDto contractDto)
    {
        return new Contract
        {
            ContractName = contractDto.ContractName,
            ProjectId = contractDto.ProjectId,
            Containers = contractDto.RevisionContainers.ToModel(),
        };
    }

    public static List<Contract> ToModel(this List<ContractDto>? contractDtos)
    {
        if (contractDtos is null) { return new List<Contract>(); }
        return contractDtos.Select(ToModel).ToList();
    }
}
