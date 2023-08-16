using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace api.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<RevisionContainer> RevisionContainers { get; set; }
    public DbSet<RevisionContainerTagNo> RevisionContainerTagNos { get; set; }
    public DbSet<RevisionContainerReview> RevisionContainerReviews { get; set; }
    public DbSet<TagDataReview> TagDataReviews { get; set; }
    public DbSet<Project> Projects { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     var connectionStringBuilder = new SqliteConnectionStringBuilder
    //     {
    //         DataSource = "C:\\Workspace\\database.db"
    //     };

    //     var connectionString = connectionStringBuilder.ToString();

    //     var connection = new SqliteConnection(connectionString);

    //     optionsBuilder.UseSqlite(connection);


    //     // optionsBuilder.UseSqlite("Data Source=C:\\Workspace\\database.db");
    // }
}
