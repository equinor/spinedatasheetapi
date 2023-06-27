namespace datasheetapi.Models;

public class Project : BaseEntity
{
    public List<Contract> Contracts{ get; set; } = new List<Contract>();
}
