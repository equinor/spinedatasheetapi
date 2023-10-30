using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos.TagReviewer;
public record TagReviewerDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string TagNo { get; set; } = string.Empty;
    public TagReviewerStateEnumDto State { get; set; }
    public Guid ContainerReviewId { get; set; }
}
