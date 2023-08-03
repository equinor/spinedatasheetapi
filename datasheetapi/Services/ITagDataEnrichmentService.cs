namespace datasheetapi.Services
{
    public interface ITagDataEnrichmentService
    {
        Task<ITagDataDto> AddRevisionContainer(ITagDataDto tagDataDto);
        Task<List<ITagDataDto>> AddRevisionContainer(List<ITagDataDto> tagDataDto);
        Task<ITagDataDto> AddReview(ITagDataDto tagDataDto);
        Task<List<ITagDataDto>> AddReview(List<ITagDataDto> tagDataDto);
    }
}