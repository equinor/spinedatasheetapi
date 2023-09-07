using AutoMapper;

using datasheetapi.Adapters;
using datasheetapi.Repositories;

namespace datasheetapi.Services;

public class CommentService : ICommentService
{
    private readonly ILogger<ContractService> _logger;
    private readonly ITagDataService _tagDataService;
    private readonly ITagDataReviewService _tagDataReviewService;
    private readonly IRevisionContainerReviewService _revisionContainerReviewService;
    private readonly ICommentRepository _commentRepository;
    private readonly IAzureUserCacheService _azureUserCacheService;
    private readonly IFusionService _fusionService;
    private readonly IMapper _mapper;

    public CommentService(ILoggerFactory loggerFactory,
        ITagDataService tagDataService,
        ICommentRepository commentRepository,
        IAzureUserCacheService azureUserCacheService,
        IFusionService fusionService,
        ITagDataReviewService tagDataReviewService,
        IRevisionContainerReviewService revisionContainerReviewService,
        IMapper mapper)
    {
        _logger = loggerFactory.CreateLogger<ContractService>();
        _tagDataService = tagDataService;
        _commentRepository = commentRepository;
        _azureUserCacheService = azureUserCacheService;
        _fusionService = fusionService;
        _tagDataReviewService = tagDataReviewService;
        _revisionContainerReviewService = revisionContainerReviewService;
        _mapper = mapper;
    }

    public async Task<GetMessageDto?> GetComment(Guid id)
    {
        var comment = await _commentRepository.GetComment(id);

        if (comment != null)
        {
            var commentResponse = _mapper.Map<GetMessageDto>(comment);
            commentResponse.CommenterName = await GetUserName(comment.UserId);
            return commentResponse;
        }
        return null;
    }

    public async Task<Conversation?> GetConversation(Guid conversationId)
    {
        var comment = await _commentRepository.GetConversation(conversationId);
        return comment;
        // if (comment == null) { return null; }
        // var username = GetUserName(comment);
        //  if (username == null) { return null; }
        //TODO: Need to fix this
        // return comment?.ToDtoOrNull();
    }

    public async Task<List<GetMessageDto>?> GetComments(Guid converstionId)
    {
        var comments = await _commentRepository.GetComments(converstionId);
        // Todo: fix add the username in the adapter
        //await AddUserNameToComments(comments);
        //return comments;
        if (comments == null) return null;
        var commentReponses = new List<GetMessageDto>();
        foreach (var comment in comments)
        {
            commentReponses.Add(await GetMessageDto(comment));
        }
        return commentReponses;
    }

    private async Task<GetMessageDto> GetMessageDto(Message comment)
    {
        var commentResponse = _mapper.Map<GetMessageDto>(comment);
        commentResponse.CommenterName = await GetUserName(comment.UserId);
        return commentResponse;
    }


    public async Task<List<Conversation>> GetConversations(Guid reviewId)
    {
        var comments = await _commentRepository.GetConversations(reviewId);
        return comments;
    }

    private async Task<string> GetUserName(Guid userId)
    {
        var azureUser = await _azureUserCacheService.GetAzureUserAsync(userId);
        if (azureUser == null)
        {
            var user = await _fusionService.ResolveUserFromPersonId(userId);
            if (user != null)
            {
                azureUser = new AzureUser { AzureUniqueId = userId, Name = user?.Name };
                _azureUserCacheService.AddAzureUser(azureUser);
            }
        }
        if (azureUser != null)
        {
            return azureUser.Name ?? "Unknown user";
        }
        else
        {
            throw new Exception("Unable to find the username for the userId: " + userId);
        }
    }

    public async Task<Conversation> CreateTagDataReviewComment(Conversation comment, Guid azureUniqueId)
    {
        if (comment.TagDataReviewId == null || comment.TagDataReviewId == Guid.Empty) { throw new Exception("Invalid tag data review id"); }
        var tagDataReview = await _tagDataReviewService.GetTagDataReview((Guid)comment.TagDataReviewId) ?? throw new Exception("Invalid tag data review");

        comment.SetTagDataReview(tagDataReview);

        return await CreateComment(comment, azureUniqueId);
    }

    //TODO: fix how to create conversation
    private async Task<Conversation> CreateComment(Conversation comment, Guid azureUniqueId)
    {
        Conversation? savedComment = null;
        // TODO: why do we need to set here 
        //comment.Messages.UserId = azureUniqueId;

        if (comment.ConversationLevel == ConversationLevel.Tag)
        {
            savedComment = await _commentRepository.CreateConversation(comment);
            // savedComment = await _commentRepository.AddComment(comment.Messages[0]);
        }
        else if (comment.ConversationLevel == ConversationLevel.PurchaserRequirement && comment.Property != null)
        {
            if (ValidateProperty<InstrumentPurchaserRequirement>(comment.Property))
            {
                savedComment = await _commentRepository.CreateConversation(comment);
            }
        }
        else if (comment.ConversationLevel == ConversationLevel.SupplierOfferedValue && comment.Property != null)
        {
            if (ValidateProperty<InstrumentSupplierOfferedProduct>(comment.Property))
            {
                savedComment = await _commentRepository.CreateConversation(comment);
            }
        }

        if (savedComment == null)
        {
            throw new Exception("Invalid comment");
        }

        return savedComment;
    }

    public async Task DeleteComment(Guid id, Guid azureUniqueId)
    {
        if (azureUniqueId == Guid.Empty) { throw new Exception("Invalid azure unique id"); }
        var comment = await _commentRepository.GetComment(id) ?? throw new Exception("Invalid comment id");

        if (comment.UserId != azureUniqueId) { throw new Exception("User not author of this comment"); }

        await _commentRepository.DeleteComment(comment);
    }

    public async Task<CommentDto?> UpdateComment(Guid azureUniqueId, Message updatedComment)
    {
        var existingComment = await _commentRepository.GetComment(updatedComment.Id)
                ?? throw new Exception($"Comment with id {updatedComment.Id} not found");

        if (existingComment.UserId != azureUniqueId) { throw new Exception("User not author of this comment"); }

        existingComment.Text = updatedComment.Text;
        existingComment.IsEdited = true;

        var comment = await _commentRepository.UpdateComment(existingComment);
        //TODO: Fix the commenter name
        return comment.ToDtoOrNull("");
    }

    private static bool ValidateProperty<T>(string propertyName)
    where T : class, new()
    {
        var obj = new T();
        var propertyInfo = obj.GetType().GetProperty(propertyName);

        return propertyInfo != null;
    }

    public async Task<Message> AddComment(Message message)
    {
        var conversation = await _commentRepository.GetConversation((Guid)message.ConversationId) ?? throw new Exception("Invalid conversation");

        message.SetConversation(conversation);

        return await _commentRepository.AddComment(message);
    }

    public async Task<Conversation> CreateConversation(Conversation conversation)
    {
        //  var commentModel = comment.ToModelOrNull() ?? throw new Exception("Invalid comment");
        //var savedComment = await CreateTagDataReviewComment(commentModel, Guid.Empty);

        if (conversation.TagDataReviewId == null || conversation.TagDataReviewId == Guid.Empty) { throw new Exception("Invalid tag data review id"); }
        var tagDataReview = await _tagDataReviewService.GetTagDataReview((Guid)conversation.TagDataReviewId) ?? throw new Exception("Invalid tag data review");

        conversation.SetTagDataReview(tagDataReview);

        return await CreateComment(conversation, Guid.Empty);

        //return savedComment.ToDtoOrNull() ?? throw new Exception("Invalid comment");
    }
}
