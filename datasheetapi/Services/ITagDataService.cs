namespace datasheetapi.Services;

public interface ITagDataService
{
    Task<ITagDataDto?> GetTagDataDtoByTagNo(string tagNo);
    Task<ITagData?> GetTagDataByTagNo(string tagNo);
    Task<List<ITagDataDto>> GetAllTagDataDtos();
    Task<List<ITagData>> GetAllTagData();
    Task<List<ITagDataDto>> GetTagDataDtosForProject(Guid id);
}
