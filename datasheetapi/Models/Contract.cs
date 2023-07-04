using System.Text.Json.Serialization;

namespace datasheetapi.Models;

public class Contract : BaseEntity
{
    public Contract(Project project)
    {
        Project = project;
        AddContractToProject(project);
    }

    private void AddContractToProject(Project project)
    {
        project.Contracts.Add(this);
    }

    public string ContractName { get; set; } = string.Empty;
    public Guid ContractorId { get; set; }

    // Relationships
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public List<RevisionContainer> RevisionContainers { get; set; } = new List<RevisionContainer>();
}