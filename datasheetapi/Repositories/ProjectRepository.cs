using api.Models;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;
public class ProjectRepository : IProjectRepository
{
    private readonly ILogger<ProjectRepository> _logger;
    private readonly DatabaseContext _context;

    public ProjectRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<ProjectRepository>();
        _context = context;
    }

    public async Task<Project?> GetProject(Guid id)
    {
        var project = await _context.Projects!.FindAsync(id);
        return project;
    }

    public async Task<List<Project>> GetProjects()
    {
        var projects = await _context.Projects!.ToListAsync();
        return projects;
    }
}
