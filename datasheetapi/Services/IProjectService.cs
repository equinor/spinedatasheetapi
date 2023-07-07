namespace datasheetapi.Services;

public interface IProjectService
{
    Task<Project?> GetProject(Guid id);
    Task<List<Project>> GetProjects();
}
