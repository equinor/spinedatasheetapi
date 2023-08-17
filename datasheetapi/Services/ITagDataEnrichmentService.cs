namespace datasheetapi.Services
{
    public interface ITagDataEnrichmentService
    {
        Task<ITagDataDto> AddRevisionContainerWithReview(ITagDataDto tagDataDto);
        Task<List<ITagDataDto>> AddRevisionContainerWithReview(List<ITagDataDto> tagDataDto);
        Task<ITagDataDto> AddReview(ITagDataDto tagDataDto);
        Task<List<ITagDataDto>> AddReview(List<ITagDataDto> tagDataDto);
    }
}
