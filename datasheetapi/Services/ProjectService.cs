using datasheetapi.Adapters;
using datasheetapi.Exceptions;
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

    public async Task<Project> GetProject(Guid projectId)
    {
        return await _projectRepository.GetProject(projectId) ??
            throw new NotFoundException($"Unable to find project - {projectId}.");
    }

    public async Task<List<Project>> GetProjects()
    {
        return await _projectRepository.GetProjects();
    }
}
