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
    public DbSet<Container> Containers { get; set; } = null!;
    public DbSet<ContainerTags> ContainerTags { get; set; } = null!;
    public DbSet<ContainerReview> ContainerReviews { get; set; } = null!;
    public DbSet<ContainerReviewer> ContainerReviewers { get; set; } = null!;
    public DbSet<TagReviewer> TagReviewers { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participant>()
            .HasKey(e => new { e.UserId, e.ConversationId });

        //modelBuilder.Entity<ContainerReviewer>()
        //    .HasKey(e => new { e.UserId, e.ContainerReview });
    }
}
