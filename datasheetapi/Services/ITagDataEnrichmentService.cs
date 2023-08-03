namespace datasheetapi.Services
{
    public interface ITagDataEnrichmentService
    {
        Task<List<ITagDataDto>> AddRevisionContainer(List<ITagDataDto> tagDataDto);
        Task<List<ITagDataDto>> AddReview(List<ITagDataDto> tagDataDto);
    }
}