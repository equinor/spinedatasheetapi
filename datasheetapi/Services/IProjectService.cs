namespace datasheetapi.Services;

public interface IProjectService
{
    Task<Project> GetProject(Guid projectId);
    Task<List<Project>> GetProjects();
}
