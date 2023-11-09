namespace datasheetapi.Services;

public interface ITagDataService
{
    Task<ITagData> GetTagDataByTagNo(string tagNo);
    Task<List<ITagData>> GetTagDataByTagNos(List<string> tagNos);
    Task<List<ITagData>> GetAllTagData();
}
