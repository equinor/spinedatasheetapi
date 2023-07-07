using datasheetapi.Repositories;

namespace datasheetapi.Services;
public class ProjectService : IProjectService
{
    private readonly ILogger<ProjectService> _logger;
    private readonly IProjectRepository _projectRepository;

    public ProjectService(ILoggerFactory loggerFactory,
        IProjectRepository projectRepository)
    {
        _logger = loggerFactory.CreateLogger<ProjectService>();
        _projectRepository = projectRepository;
    }

    public async Task<Project?> GetProject(Guid id)
    {
        return await _projectRepository.GetProject(id);
    }

    public async Task<List<Project>> GetProjects()
    {
        return await _projectRepository.GetProjects();
    }
}
