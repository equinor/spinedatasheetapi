using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace datasheetapi.Models;

public class RevisionContainerTagNo : BaseEntity
{
    public string TagNo { get; set; } = null!;
    public RevisionContainer RevisionContainer { get; set; } = null!;
}
