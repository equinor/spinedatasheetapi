namespace datasheetapi.Services
{
    public interface IEnrichTagDataService
    {
        Task<List<ITagDataDto>> AddRevisionContainer(List<ITagDataDto> tagDataDto);
        Task<List<ITagDataDto>> AddReview(List<ITagDataDto> tagDataDto);
    }
}