using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace datasheetapi.Models;

public class RevisionContainer : BaseEntity
{
    public string RevisionContainerName { get; set; } = string.Empty;
    public int RevisionNumber { get; set; }
    public DateTimeOffset RevisionContainerDate { get; set; } = DateTimeOffset.Now;
    public List<RevisionContainerTagNo> TagNo { get; set; } = new List<RevisionContainerTagNo>();
    public RevisionContainerReview? RevisionContainerReview { get; set; }
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }
}