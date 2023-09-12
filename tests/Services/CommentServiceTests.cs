using datasheetapi.Adapters;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Fusion.Integration.Profile;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;

namespace tests.Services;
public class CommentServiceTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock = new();
    private readonly Mock<ICommentRepository> _commentRepositoryMock = new();
    private readonly Mock<IAzureUserCacheService> _azureUserCacheServiceMock = new();
    private readonly Mock<IFusionService> _fusionServiceMock = new();
    private readonly Mock<ITagDataReviewService> _tagDataReviewServiceMock = new();

    private readonly CommentService _commentService;

    public CommentServiceTests()
    {
        _commentService = new CommentService(
            _loggerFactoryMock.Object,
            _commentRepositoryMock.Object,
            _azureUserCacheServiceMock.Object,
            _fusionServiceMock.Object,
            _tagDataReviewServiceMock.Object);
    }

    public static Message SetUpComment()
    {
        var commentId = Guid.NewGuid();
        var comment = new Message { Id = commentId, UserId = Guid.NewGuid() };
        return comment;
    }

    public static Conversation SetUpConversation()
    {
        var conversationId = Guid.NewGuid();
        var conversation = new Conversation { Id = conversationId, TagDataReviewId = Guid.NewGuid() };
        return conversation;
    }

    [Fact]
    public async Task CreateConversation_ThrowsWhenUnableToFetchTagReview()
    {
        var conversation = SetUpConversation();

        _tagDataReviewServiceMock.Setup(x => x.GetTagDataReview(conversation.TagDataReviewId)).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(() => _commentService.CreateConversation(conversation));
    }

    [Fact]
    public async Task CreateConversation_ThrowsWhenReviewIdNotfound()
    {
        var conversation = SetUpConversation();

        _tagDataReviewServiceMock.Setup(x => x.GetTagDataReview(conversation.TagDataReviewId)).ReturnsAsync((TagDataReview?)null);

        await Assert.ThrowsAsync<Exception>(() => _commentService.CreateConversation(conversation));
    }

    [Fact]
    public async Task CreateConversation_ThrowsSavingConversationThrowsException()
    {
        var conversation = SetUpConversation();

        var tagDataReview = new TagDataReview { Id = conversation.TagDataReviewId };
        _tagDataReviewServiceMock.Setup(x => x.GetTagDataReview(conversation.TagDataReviewId)).ReturnsAsync(tagDataReview);
        _commentRepositoryMock.Setup(x => x.CreateConversation(conversation)).ThrowsAsync(new DbUpdateException());

        await Assert.ThrowsAsync<DbUpdateException>(() => _commentService.CreateConversation(conversation));
    }

    [Fact]
    public async Task CreateConversation_RunsOkayWithCorrectInput()
    {
        var conversation = SetUpConversation();

        var tagDataReview = new TagDataReview { Id = conversation.TagDataReviewId };
        _tagDataReviewServiceMock.Setup(x => x.GetTagDataReview(conversation.TagDataReviewId)).ReturnsAsync(tagDataReview);
        _commentRepositoryMock.Setup(x => x.CreateConversation(conversation)).ReturnsAsync(conversation);

        var result = await _commentService.CreateConversation(conversation);

        Assert.NotNull(result);
        Assert.Equal(conversation.Id, result.Id);
        _commentRepositoryMock.Verify(x => x.CreateConversation(conversation), Times.Once);
    }

    [Fact]
    public async Task GetConversation_ThrowsWhenFetchingConversationThrowsException()
    {
        var conversation = SetUpConversation();

        _commentRepositoryMock.Setup(x => x.GetConversation(conversation.Id)).ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() => _commentService.GetConversation(conversation.Id));
    }

    [Fact]
    public async Task GetConversation_RunsOkayWithCorrectInput()
    {
        var conversation = SetUpConversation();

        _commentRepositoryMock.Setup(x => x.GetConversation(conversation.Id)).ReturnsAsync(conversation);

        var result = await _commentService.GetConversation(conversation.Id);

        Assert.NotNull(result);
        Assert.Equal(conversation.Id, result.Id);
        _commentRepositoryMock.Verify(x => x.GetConversation(conversation.Id), Times.Once);
    }

    [Fact]
    public async Task GetConversations_ThrowsWhenFetchingConversationsThrowsException()
    {
        var conversation = SetUpConversation();

        _commentRepositoryMock.Setup(x => x.GetConversations(conversation.TagDataReviewId)).ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() => _commentService.GetConversations(conversation.TagDataReviewId));
    }

    [Fact]
    public async Task GetConversations_RunsOkayWithCorrectInput()
    {
        var conversation = SetUpConversation();

        _commentRepositoryMock.Setup(x => x.GetConversations(conversation.TagDataReviewId)).ReturnsAsync(new List<Conversation> { conversation });

        var result = await _commentService.GetConversations(conversation.TagDataReviewId);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(conversation.Id, result[0].Id);
        _commentRepositoryMock.Verify(x => x.GetConversations(conversation.TagDataReviewId), Times.Once);
    }

    [Fact]
    public async Task AddComment_ThrowsWhenUnableToFetchConversation()
    {
        var comment = SetUpComment();
        var conversationId = Guid.NewGuid();
        _commentRepositoryMock.Setup(x => x.GetConversation(conversationId)).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(() => _commentService.AddComment(conversationId, comment));
    }

    [Fact]
    public async Task AddComment_ThrowsWhenConversationIdNotfound()
    {
        var comment = SetUpComment();
        var conversationId = Guid.NewGuid();

        _commentRepositoryMock.Setup(x => x.GetConversation(conversationId)).ReturnsAsync((Conversation?)null);

        await Assert.ThrowsAsync<Exception>(() => _commentService.AddComment(conversationId, comment));
    }

    [Fact]
    public async Task AddComment_ThrowsWhenSavingCommentThrowsException()
    {
        var comment = SetUpComment();
        var conversationId = Guid.NewGuid();

        _commentRepositoryMock.Setup(x => x.GetConversation(conversationId)).ReturnsAsync(SetUpConversation());
        _commentRepositoryMock.Setup(x => x.AddComment(comment)).ThrowsAsync(new DbUpdateException());

        await Assert.ThrowsAsync<DbUpdateException>(() => _commentService.AddComment(conversationId, comment));
    }

    [Fact]
    public async Task AddComment_RunsOkayWithCorrectInput()
    {
        var comment = SetUpComment();
        var conversationId = Guid.NewGuid();

        _commentRepositoryMock.Setup(x => x.GetConversation(conversationId)).ReturnsAsync(SetUpConversation());
        _commentRepositoryMock.Setup(x => x.AddComment(comment)).ReturnsAsync(comment);

        var result = await _commentService.AddComment(conversationId, comment);

        Assert.NotNull(result);
        Assert.Equal(comment.Id, result.Id);
        _commentRepositoryMock.Verify(x => x.AddComment(comment), Times.Once);
    }

    [Fact]
    public async Task GetComment_ThrowsWhenFetchingCommentThrowsException()
    {
        var comment = SetUpComment();

        _commentRepositoryMock.Setup(x => x.GetComment(comment.Id)).ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() => _commentService.GetComment(comment.Id));
    }

    [Fact]
    public async Task GetComment_RunsOkayWithCorrectInput()
    {
        var comment = SetUpComment();

        _commentRepositoryMock.Setup(x => x.GetComment(comment.Id)).ReturnsAsync(comment);

        var result = await _commentService.GetComment(comment.Id);

        Assert.NotNull(result);
        Assert.Equal(comment.Id, result.Id);
        _commentRepositoryMock.Verify(x => x.GetComment(comment.Id), Times.Once);
    }

    [Fact]
    public async Task GetComments_ThrowsWhenFetchingCommentsThrowsException()
    {
        var conversationId = Guid.NewGuid();

        _commentRepositoryMock.Setup(x => x.GetComments(conversationId))
            .ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _commentService.GetComments(conversationId));
    }

    [Fact]
    public async Task GetComments_RunsOkayWithCorrectInput()
    {
        var comment = SetUpComment();
        var conversationId = Guid.NewGuid();
        comment.ConversationId = conversationId;

        _commentRepositoryMock.Setup(x => x.GetComments(conversationId)).ReturnsAsync(new List<Message> { comment });

        var result = await _commentService.GetComments(conversationId);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(comment.Id, result[0].Id);
        Assert.Equal(conversationId, result[0].ConversationId);
        _commentRepositoryMock.Verify(x => x.GetComments(conversationId), Times.Once);
    }

    [Fact]
    public async Task DeleteComment_ThrowsExceptionWhenAzureUserIdEmpty()
    {
        // Arrange
        var comment = SetUpComment();

        await Assert.ThrowsAsync<Exception>(() =>
                _commentService.DeleteComment(comment.Id, Guid.Empty));
    }

    [Fact]
    public async Task DeleteComment_ThrowsExceptionWhenCommentIsNotFound()
    {
        // Arrange
        var comment = SetUpComment();

        _commentRepositoryMock.Setup(x => x.GetComment(comment.Id))
                    .ReturnsAsync((Message?)null);
        await Assert.ThrowsAsync<Exception>(() =>
                _commentService.DeleteComment(comment.Id, Guid.Empty));
    }

    [Fact]
    public async Task DeleteComment_ThrowsException_WhenDeletingUserNotSameAsCreatedUser()
    {
        // Arrange
        var comment = SetUpComment();

        _commentRepositoryMock.Setup(x => x.GetComment(comment.Id))
                    .ReturnsAsync(comment);
        await Assert.ThrowsAsync<Exception>(() =>
                _commentService.DeleteComment(comment.Id, Guid.NewGuid()));
    }

    [Fact]
    public async Task DeleteComment_RunsOk()
    {
        var comment = SetUpComment();

        _commentRepositoryMock.Setup(x => x.GetComment(comment.Id)).ReturnsAsync(comment);
        _commentRepositoryMock.Setup(x => x.UpdateComment(comment));

        await _commentService.DeleteComment(comment.Id, comment.UserId);

        _commentRepositoryMock.Verify(x => x.UpdateComment(comment), Times.Once);
    }

    [Fact]
    public async Task UpdateComment_RunsOk()
    {
        var updatedComment = "Updating text";
        var comment = SetUpComment();
        comment.Text = updatedComment;

        _commentRepositoryMock.Setup(x => x.GetComment(comment.Id)).ReturnsAsync(comment);
        _commentRepositoryMock.Setup(x => x.UpdateComment(comment)).ReturnsAsync(comment);

        var result = await _commentService.UpdateComment(comment.Id, comment);

        Assert.NotNull(result);
        Assert.Equal(result.Text, updatedComment);
        Assert.True(result.IsEdited);
        _commentRepositoryMock.Verify(x => x.UpdateComment(comment), Times.Once);
    }

    [Fact]
    public async Task UpdateComment_ThrowsException_WhenUpdatingUserNotSameAsCreatedUser()
    {
        // Arrange
        var comment = SetUpComment();

        var updatedComment = SetUpComment();
        updatedComment.UserId = Guid.NewGuid();

        _commentRepositoryMock.Setup(x => x.GetComment(comment.Id))
                    .ReturnsAsync(comment);
        await Assert.ThrowsAsync<Exception>(() =>
                _commentService.UpdateComment(comment.Id, updatedComment));
    }

    [Fact]
    public async Task GetUserName_FetchUserNameFromCacheWhenCacheIsLoaded()
    {

        var userId = Guid.NewGuid();
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(userId))
                    .ReturnsAsync(new AzureUser { AzureUniqueId = userId, Name = "Test User" });

        await _commentService.GetUserName(userId);

        _fusionServiceMock.Verify(x => x.ResolveUserFromPersonId(userId), Times.Never);
        _azureUserCacheServiceMock.Verify(x => x.GetAzureUserAsync(userId), Times.Once);
    }

    [Fact]
    public async Task GetUserName_FetchUserNameFromFusion()
    {

        var userId = Guid.NewGuid();
        var userName = "Name";
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(userId))
                    .ReturnsAsync((AzureUser?)null);
        _fusionServiceMock.Setup(x => x.ResolveUserFromPersonId(userId))
                     .ReturnsAsync(new FusionPersonProfile(FusionAccountType.Employee, "upn", Guid.NewGuid(), userName));
        var result = await _commentService.GetUserName(userId);

        Assert.NotNull(result);
        Assert.Equal(userName, result);
        _fusionServiceMock.Verify(x => x.ResolveUserFromPersonId(userId), Times.Once);
        _azureUserCacheServiceMock.Verify(x => x.GetAzureUserAsync(userId), Times.Once);
    }

    [Fact]
    public async Task GetUserName_ThrowsException_whenUnableToFindUser()
    {

        var userId = Guid.NewGuid();
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(userId))
                    .ReturnsAsync((AzureUser?)null);
        _fusionServiceMock.Setup(x => x.ResolveUserFromPersonId(userId))
                     .ReturnsAsync((FusionPersonProfile?)null);
        await Assert.ThrowsAsync<Exception>(() => _commentService.GetUserName(userId));
    }

    [Fact]
    public async Task GetUserIdUserName_RunsOkay()
    {

        var userId = Guid.NewGuid();
        var userName = "Name";
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(userId))
                    .ReturnsAsync((AzureUser?)null);
        _fusionServiceMock.Setup(x => x.ResolveUserFromPersonId(userId))
                     .ReturnsAsync(new FusionPersonProfile(FusionAccountType.Employee, "upn", Guid.NewGuid(), userName));
        var result = await _commentService.GetUserIdUserName(new List<Guid> { userId });

        Assert.NotNull(result);
        Assert.Equal(userName, result[userId]);
    }
}
