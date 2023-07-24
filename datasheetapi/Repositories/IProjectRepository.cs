namespace datasheetapi.Repositories;
public interface IProjectRepository
{
    Task<Project?> GetProject(Guid id);
    Task<List<Project>> GetProjects();
}
