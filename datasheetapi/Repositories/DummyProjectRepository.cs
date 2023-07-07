namespace datasheetapi.Repositories;
public class DummyProjectRepository : IProjectRepository
{
    private readonly List<Project> _projects = new();
    private readonly ILogger<DummyProjectRepository> _logger;

    public DummyProjectRepository(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DummyProjectRepository>();
    }

    public Task<Project?> GetProject(Guid id)
    {
        return Task.FromResult<Project?>(null);
    }

    public Task<List<Project>> GetProjects()
    {
        return Task.FromResult(new List<Project>());
    }
}
