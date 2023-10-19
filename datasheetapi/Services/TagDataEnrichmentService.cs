using datasheetapi.Adapters;

namespace datasheetapi.Services;

public class TagDataEnrichmentService : ITagDataEnrichmentService
{
    private readonly IRevisionContainerService _revisionContainerService;
    private readonly IUserService _userService;

    public TagDataEnrichmentService(
        IRevisionContainerService revisionContainerService,
        IUserService userService)
    {
        _revisionContainerService = revisionContainerService;
        _userService = userService;
    }

    public Task<ITagDataDto> AddRevisionContainerWithReview(ITagDataDto tagDataDto)
    {
        throw new NotImplementedException();
    }

    public Task<List<ITagDataDto>> AddRevisionContainerWithReview(List<ITagDataDto> tagDataDto)
    {
        throw new NotImplementedException();
    }
}
