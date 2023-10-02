using datasheetapi.Adapters;
using datasheetapi.Exceptions;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Fusion.Integration.Profile;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Common;

using Moq;

namespace tests.Services;
public class ConversationServiceTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock = new();
    private readonly Mock<IConversationRepository> _conversationRepositoryMock = new();
    private readonly Mock<IAzureUserCacheService> _azureUserCacheServiceMock = new();
    private readonly Mock<IFusionService> _fusionServiceMock = new();
    private readonly Mock<ITagDataReviewService> _tagDataReviewServiceMock = new();

    private readonly ConversationService _conversatiosnService;

    public ConversationServiceTests()
    {
        _conversatiosnService = new ConversationService(
            _loggerFactoryMock.Object,
            _conversationRepositoryMock.Object,
            _azureUserCacheServiceMock.Object,
            _fusionServiceMock.Object,
            _tagDataReviewServiceMock.Object);
    }

    public static Message SetUpMessage()
    {
        var messageId = Guid.NewGuid();
        return new Message { Id = messageId, UserId = Guid.NewGuid(), ConversationId = Guid.NewGuid() };
    }

    public static Conversation SetUpConversation()
    {
        var conversationId = Guid.NewGuid();
        var conversation = new Conversation { Id = conversationId, TagDataReviewId = Guid.NewGuid() };
        return conversation;
    }

    public static AzureUser SetUpAzureUser()
    {
        return new AzureUser { AzureUniqueId = Guid.NewGuid(), Name = "some name" };
    }

    [Fact]
    public async Task CreateConversation_ThrowsWhenUnableToFetchTagReview()
    {
        var conversation = SetUpConversation();

        _tagDataReviewServiceMock.Setup(x => x.GetTagDataReview(conversation.TagDataReviewId)).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(() => _conversatiosnService.CreateConversation(conversation));
    }

    [Fact]
    public async Task CreateConversation_ThrowsWhenReviewIdNotfound()
    {
        var conversation = SetUpConversation();

        _tagDataReviewServiceMock.Setup(x => x.GetTagDataReview(conversation.TagDataReviewId))
            .ThrowsAsync(new NotFoundException("Unbale to find the TagDataReview"));

        await Assert.ThrowsAsync<NotFoundException>(() => _conversatiosnService.CreateConversation(conversation));
    }

    [Fact]
    public async Task CreateConversation_ThrowsSavingConversationThrowsException()
    {
        var conversation = SetUpConversation();

        _tagDataReviewServiceMock.Setup(x =>
            x.GetTagDataReview(conversation.TagDataReviewId))
                .ReturnsAsync(new TagDataReview { Id = conversation.TagDataReviewId });
        _conversationRepositoryMock.Setup(x => x.CreateConversation(conversation)).ThrowsAsync(new DbUpdateException());

        await Assert.ThrowsAsync<DbUpdateException>(() => _conversatiosnService.CreateConversation(conversation));
    }

    [Fact]
    public async Task CreateConversation_RunsOkayWithCorrectInput()
    {
        var conversation = SetUpConversation();

        _tagDataReviewServiceMock.Setup(x =>
                x.GetTagDataReview(conversation.TagDataReviewId))
                    .ReturnsAsync(new TagDataReview { Id = conversation.TagDataReviewId });
        _conversationRepositoryMock.Setup(x => x.CreateConversation(conversation)).ReturnsAsync(conversation);

        var result = await _conversatiosnService.CreateConversation(conversation);

        Assert.NotNull(result);
        Assert.Equal(conversation.Id, result.Id);
        _conversationRepositoryMock.Verify(x => x.CreateConversation(conversation), Times.Once);
    }

    [Fact]
    public async Task GetConversation_ThrowsWhenFetchingConversationThrowsException()
    {
        var conversation = SetUpConversation();

        _conversationRepositoryMock.Setup(x => x.GetConversation(conversation.Id)).ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() => _conversatiosnService.GetConversation(conversation.Id));
    }

    [Fact]
    public async Task GetConversation_RunsOkayWithCorrectInput()
    {
        var conversation = SetUpConversation();

        _conversationRepositoryMock.Setup(x => x.GetConversation(conversation.Id)).ReturnsAsync(conversation);

        var result = await _conversatiosnService.GetConversation(conversation.Id);

        Assert.NotNull(result);
        Assert.Equal(conversation.Id, result.Id);
        _conversationRepositoryMock.Verify(x => x.GetConversation(conversation.Id), Times.Once);
    }

    [Fact]
    public async Task GetConversations_ThrowsWhenFetchingConversationsThrowsException()
    {
        var conversation = SetUpConversation();

        _conversationRepositoryMock.Setup(x => x.GetConversations(conversation.TagDataReviewId)).ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _conversatiosnService.GetConversations(conversation.TagDataReviewId, false));
    }

    [Fact]
    public async Task GetConversations_RunsOkayWithCorrectInput()
    {
        var conversation = SetUpConversation();

        _conversationRepositoryMock.Setup(x => x.GetConversations(conversation.TagDataReviewId)).ReturnsAsync(new List<Conversation> { conversation });

        var result = await _conversatiosnService.GetConversations(conversation.TagDataReviewId, false);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(conversation.Id, result[0].Id);
        _conversationRepositoryMock.Verify(x => x.GetConversations(conversation.TagDataReviewId), Times.Once);
    }

    [Fact]
    public async Task GetConversationsWithLatestMessage_RunsOkay()
    {
        var conversation = SetUpConversation();

        _conversationRepositoryMock.Setup(x => x.GetConversationsWithLatestMessage(conversation.TagDataReviewId, false))
            .ReturnsAsync(new List<Conversation> { conversation });

        var result = await _conversatiosnService.GetConversations(conversation.TagDataReviewId, true);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(conversation.Id, result[0].Id);
        _conversationRepositoryMock.Verify(x => x.GetConversationsWithLatestMessage(conversation.TagDataReviewId, false), Times.Once);
    }

    [Fact]
    public async Task AddMessage_ThrowsWhenUnableToFetchConversation()
    {
        var message = SetUpMessage();
        _conversationRepositoryMock.Setup(x => x.GetConversation(message.ConversationId))
                .ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(() => _conversatiosnService
                .AddMessage(message.ConversationId, message));
    }

    [Fact]
    public async Task AddMessage_ThrowsWhenConversationIdNotfound()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetConversation(message.ConversationId))
                    .ReturnsAsync((Conversation?)null);

        await Assert.ThrowsAsync<Exception>(() => _conversatiosnService
                    .AddMessage(message.ConversationId, message));
    }

    [Fact]
    public async Task AddMessage_ThrowsWhenSavingMessageThrowsException()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetConversation(message.ConversationId))
                .ReturnsAsync(SetUpConversation());
        _conversationRepositoryMock.Setup(x => x.AddMessage(message)).ThrowsAsync(new DbUpdateException());

        await Assert.ThrowsAsync<DbUpdateException>(() => _conversatiosnService
            .AddMessage(message.ConversationId, message));
    }

    [Fact]
    public async Task AddMessage_RunsOkayWithCorrectInput()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetConversation(message.ConversationId))
                    .ReturnsAsync(SetUpConversation());
        _conversationRepositoryMock.Setup(x => x.AddMessage(message)).ReturnsAsync(message);

        var result = await _conversatiosnService.AddMessage(message.ConversationId, message);

        Assert.NotNull(result);
        Assert.Equal(message.Id, result.Id);
        _conversationRepositoryMock.Verify(x => x.AddMessage(message), Times.Once);
    }

    [Fact]
    public async Task GetMessage_ThrowsWhenFetchingMessageThrowsException()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id)).ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() => _conversatiosnService.GetMessage(message.Id));
    }

    [Fact]
    public async Task GetMessage_RunsOkayWithCorrectInput()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id)).ReturnsAsync(message);

        var result = await _conversatiosnService.GetMessage(message.Id);

        Assert.NotNull(result);
        Assert.Equal(message.Id, result.Id);
        _conversationRepositoryMock.Verify(x => x.GetMessage(message.Id), Times.Once);
    }

    [Fact]
    public async Task GetMessages_ThrowsWhenFetchingMessagesThrowsException()
    {
        var conversationId = Guid.NewGuid();

        _conversationRepositoryMock.Setup(x => x.GetMessages(conversationId))
            .ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _conversatiosnService.GetMessages(conversationId));
    }

    [Fact]
    public async Task GetMesages_RunsOkayWithCorrectInput()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessages(message.ConversationId))
                    .ReturnsAsync(new List<Message> { message });

        var result = await _conversatiosnService.GetMessages(message.ConversationId);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(message.Id, result[0].Id);
        Assert.Equal(message.ConversationId, result[0].ConversationId);
        _conversationRepositoryMock.Verify(x => x.GetMessages(message.ConversationId), Times.Once);
    }

    [Fact]
    public async Task DeleteMessage_ThrowsExceptionWhenAzureUserIdEmpty()
    {
        // Arrange
        var message = SetUpMessage();

        await Assert.ThrowsAsync<BadRequestException>(() =>
                _conversatiosnService.DeleteMessage(message.Id, Guid.Empty));
    }

    [Fact]
    public async Task DeleteMessage_ThrowsNotFoundExceptionWhenMessageIsNotFound()
    {
        // Arrange
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id))
                    .ReturnsAsync((Message?)null);
        await Assert.ThrowsAsync<NotFoundException>(() =>
                _conversatiosnService.DeleteMessage(message.Id, message.UserId));
    }

    [Fact]
    public async Task DeleteMessage_ThrowsException_WhenDeletingUserNotSameAsCreatedUser()
    {
        // Arrange
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id))
                    .ReturnsAsync(message);
        await Assert.ThrowsAsync<BadRequestException>(() =>
                _conversatiosnService.DeleteMessage(message.Id, Guid.NewGuid()));
    }

    [Fact]
    public async Task DeleteMessage_RunsOk()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id)).ReturnsAsync(message);
        _conversationRepositoryMock.Setup(x => x.UpdateMessage(message));

        await _conversatiosnService.DeleteMessage(message.Id, message.UserId);

        _conversationRepositoryMock.Verify(x => x.UpdateMessage(message), Times.Once);
    }

    [Fact]
    public async Task UpdateMessage_RunsOk()
    {
        var updatedMessage = "Updating text";
        var message = SetUpMessage();
        message.Text = updatedMessage;

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id)).ReturnsAsync(message);
        _conversationRepositoryMock.Setup(x => x.UpdateMessage(message)).ReturnsAsync(message);

        var result = await _conversatiosnService.UpdateMessage(message.Id, message);

        Assert.NotNull(result);
        Assert.Equal(result.Text, updatedMessage);
        Assert.True(result.IsEdited);
        _conversationRepositoryMock.Verify(x => x.UpdateMessage(message), Times.Once);
    }

    [Fact]
    public async Task UpdateMessage_ThrowsException_WhenUpdatingUserNotSameAsCreatedUser()
    {
        // Arrange
        var message = SetUpMessage();

        var updatedMessage = SetUpMessage();
        updatedMessage.UserId = Guid.NewGuid();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id))
                    .ReturnsAsync(message);
        await Assert.ThrowsAsync<BadRequestException>(() =>
                _conversatiosnService.UpdateMessage(message.Id, updatedMessage));
    }

    [Fact]
    public async Task GetUserName_FetchUserNameFromCacheWhenCacheIsLoaded()
    {

        var userId = Guid.NewGuid();
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(userId))
                    .ReturnsAsync(new AzureUser { AzureUniqueId = userId, Name = "Test User" });

        await _conversatiosnService.GetUserName(userId);

        _fusionServiceMock.Verify(x => x.ResolveUserFromPersonId(userId), Times.Never);
        _azureUserCacheServiceMock.Verify(x => x.GetAzureUserAsync(userId), Times.Once);
    }

    [Fact]
    public async Task GetUserName_FetchUserNameFromFusion()
    {

        var user = SetUpAzureUser();

        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(user.AzureUniqueId))
                    .ReturnsAsync((AzureUser?)null);
        _fusionServiceMock.Setup(x => x.ResolveUserFromPersonId(user.AzureUniqueId))
                     .ReturnsAsync(new FusionPersonProfile(FusionAccountType.Employee,
                            "upn", Guid.NewGuid(), user.Name ?? ""));
        var result = await _conversatiosnService.GetUserName(user.AzureUniqueId);

        Assert.NotNull(result);
        Assert.Equal(user.Name, result);
        _fusionServiceMock.Verify(x => x.ResolveUserFromPersonId(user.AzureUniqueId), Times.Once);
        _azureUserCacheServiceMock.Verify(x => x.GetAzureUserAsync(user.AzureUniqueId), Times.Once);
    }

    [Fact]
    public async Task GetUserName_ThrowsException_whenUnableToFindUser()
    {

        var user = SetUpAzureUser();
        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(user.AzureUniqueId))
                    .ReturnsAsync((AzureUser?)null);
        _fusionServiceMock.Setup(x => x.ResolveUserFromPersonId(user.AzureUniqueId))
                     .ReturnsAsync((FusionPersonProfile?)null);
        await Assert.ThrowsAsync<NotFoundException>(() => _conversatiosnService.GetUserName(user.AzureUniqueId));
    }

    [Fact]
    public async Task GetUserIdUserName_RunsOkay()
    {
        var user = SetUpAzureUser();

        _azureUserCacheServiceMock.Setup(x => x.GetAzureUserAsync(user.AzureUniqueId))
                    .ReturnsAsync((AzureUser?)null);
        _fusionServiceMock.Setup(x => x.ResolveUserFromPersonId(user.AzureUniqueId))
                     .ReturnsAsync(new FusionPersonProfile(FusionAccountType.Employee,
                            "upn", Guid.NewGuid(), user.Name ?? ""));
        var result = await _conversatiosnService.GetUserIdUserName(new List<Guid> { user.AzureUniqueId });

        Assert.NotNull(result);
        Assert.Equal(user.Name, result[user.AzureUniqueId]);
    }
}
