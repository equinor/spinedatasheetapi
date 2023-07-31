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


    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<WellProjectWell>().HasKey(wc => new { wc.WellProjectId, wc.WellId });
    //     modelBuilder.Entity<WellProjectWell>().HasOne(w => w.Well).WithMany(w => w.WellProjectWells).OnDelete(DeleteBehavior.NoAction);

    //     modelBuilder.Entity<ExplorationWell>().HasKey(ew => new { ew.ExplorationId, ew.WellId });
    //     modelBuilder.Entity<ExplorationWell>().HasOne(w => w.Well).WithMany(w => w.ExplorationWells).OnDelete(DeleteBehavior.NoAction);
    // }
}
