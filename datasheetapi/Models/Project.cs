namespace datasheetapi.Models;

public class Project : BaseEntity
{
    public List<Contract> Contracts { get; set; } = new List<Contract>();
    public List<Reviewer> Reviewers { get; set; } = new List<Reviewer>();
}
