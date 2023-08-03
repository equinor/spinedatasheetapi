namespace datasheetapi.Services;

public interface ITagDataService
{
    Task<ITagDataDto?> GetTagDataDtoById(Guid id);
    Task<ITagData?> GetTagDataById(Guid id);
    Task<List<ITagDataDto>> GetAllTagDataDtos();
    Task<List<ITagData>> GetAllTagData();
    Task<List<ITagDataDto>> GetTagDataDtosForProject(Guid id);
}
