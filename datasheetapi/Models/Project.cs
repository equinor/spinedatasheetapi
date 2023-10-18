namespace datasheetapi.Models;

public class Project
{
    public Guid Id { get; set; }
    public List<Contract> Contracts { get; set; } = new List<Contract>();
}
