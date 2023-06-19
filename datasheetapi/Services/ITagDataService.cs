namespace datasheetapi.Services;

public interface ITagDataService
{
    Task<ITagDataDto?> GetTagDataById(Guid id);
    Task<List<ITagDataDto>> GetAllTagData();
    Task<ActionResult<List<ITagDataDto>>> GetTagDataForProject(Guid id);
}
