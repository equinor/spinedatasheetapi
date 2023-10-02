using Microsoft.EntityFrameworkCore;

namespace api.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }
    public DbSet<Conversation> Conversations { get; set; } = null!;
    public DbSet<Participant> Participants { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<RevisionContainer> RevisionContainers { get; set; } = null!;
    public DbSet<RevisionContainerTagNo> RevisionContainerTagNos { get; set; } = null!;
    public DbSet<RevisionContainerReview> RevisionContainerReviews { get; set; } = null!;
    public DbSet<TagDataReview> TagDataReviews { get; set; } = null!;
    public DbSet<ReviewerTagDataReview> ReviewerTagDataReviews { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participant>()
            .HasKey(e => new { e.UserId, e.ConversationId });

        modelBuilder.Entity<ReviewerTagDataReview>()
            .HasKey(e => new { e.ReviewerId, e.TagDataReviewId });
    }
}
