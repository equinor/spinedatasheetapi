namespace datasheetapi.Models;

public class ContainerTags
{
    public string TagNo { get; set; } = null!;
    public Guid ContainerId { get; set; }
    public Container Container { get; set; } = null!;
}
