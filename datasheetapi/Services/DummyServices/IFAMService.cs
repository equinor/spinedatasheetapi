namespace datasheetapi.Services;

public interface IFAMService
{
    Task<ITagData?> GetTagData(string tagNo);
    Task<List<ITagData>> GetTagDataForTagNos(List<string> tagNos);
    Task<List<ITagData>> GetTagData();
}
