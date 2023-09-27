namespace datasheetapi.Services;

public interface IFAMService
{
    Task<ITagData?> GetTagData(string tagNo);
    Task<List<ITagData>> GetTagData();
}
