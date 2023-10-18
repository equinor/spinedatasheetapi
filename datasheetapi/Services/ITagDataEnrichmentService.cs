namespace datasheetapi.Services
{
    public interface ITagDataEnrichmentService
    {
        Task<ITagDataDto> AddRevisionContainerWithReview(ITagDataDto tagDataDto);
        Task<List<ITagDataDto>> AddRevisionContainerWithReview(List<ITagDataDto> tagDataDto);
    }
}
