using Microsoft.EntityFrameworkCore;


namespace api.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }
    public DbSet<Comment>? Comments { get; set; }
    public DbSet<Contract>? Contracts { get; set; }
    public DbSet<RevisionContainer>? RevisionContainers { get; set; }
    public DbSet<RevisionContainerReview>? RevisionContainerReviews { get; set; }
    public DbSet<TagDataReview>? TagDataReviews { get; set; }
    public DbSet<Project>? Projects { get; set; }
}