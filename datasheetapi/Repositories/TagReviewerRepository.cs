using api.Database;

using Microsoft.EntityFrameworkCore;

namespace datasheetapi.Repositories;

public class TagReviewerRepository : ITagReviewerRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<TagReviewerRepository> _logger;

    public TagReviewerRepository(ILoggerFactory loggerFactory, DatabaseContext context)
    {
        _logger = loggerFactory.CreateLogger<TagReviewerRepository>();
        _context = context;
    }

    public async Task<List<TagReviewer>> CreateReviewers(List<TagReviewer> reviewers)
    {
        _context.TagReviewers.AddRange(reviewers);
        await _context.SaveChangesAsync();

        return reviewers;
    }

    public async Task<bool> AnyTagReviewerWithTagNoAndContainerReviewerId(string tagNo, Guid containerReviewerId)
    {
        var exists = await _context.TagReviewers.AnyAsync(tr =>
                tr.TagNo == tagNo && tr.ContainerReviewerId == containerReviewerId);
        return exists;
    }

    public Task<TagReviewer?> GetReviewer(Guid reviewerId)
    {
        throw new NotImplementedException();
        //var reviewer = await _context.TagReviewers.FirstOrDefaultAsync(r => r.TagDataReviewId == reviewId && r.ReviewerId == reviewerId);
        //return reviewer;
    }

    public async Task<TagReviewer> UpdateReviewer(TagReviewer reviewer)
    {
        _context.TagReviewers.Update(reviewer);

        await _context.SaveChangesAsync();

        return reviewer;
    }
}
