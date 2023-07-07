namespace datasheetapi.Repositories;
public class DummyProjectRepository : IProjectRepository
{
    private readonly List<Project> _projects = new();
    private readonly ILogger<DummyProjectRepository> _logger;

    public DummyProjectRepository(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DummyProjectRepository>();
        _projects = DummyData.GetProjects();
    }

    public Task<Project?> GetProject(Guid id)
    {
        return Task.Run(() => _projects.Find(p => p.Id == id));
    }

    public Task<List<Project>> GetProjects()
    {
        return Task.Run(() => _projects);
    }
}
