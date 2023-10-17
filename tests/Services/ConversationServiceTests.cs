using datasheetapi.Exceptions;
using datasheetapi.Models;
using datasheetapi.Repositories;
using datasheetapi.Services;

using Fusion.Integration.Profile;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;

namespace tests.Services;
public class ConversationServiceTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock = new();
    private readonly Mock<IConversationRepository> _conversationRepositoryMock = new();
    private readonly Mock<IAzureUserCacheService> _azureUserCacheServiceMock = new();
    private readonly Mock<IFusionService> _fusionServiceMock = new();
    private readonly Mock<IFAMService> _famServiceMock = new();

    private readonly ConversationService _conversationService;

    public ConversationServiceTests()
    {
        _conversationService = new ConversationService(
            _loggerFactoryMock.Object,
            _conversationRepositoryMock.Object,
            _famServiceMock.Object);
    }

    public static Message SetUpMessage()
    {
        var messageId = Guid.NewGuid();
        return new Message { Id = messageId, UserId = Guid.NewGuid(), ConversationId = Guid.NewGuid() };
    }

    public static Conversation SetUpConversation()
    {
        var conversationId = Guid.NewGuid();
        var conversation = new Conversation
        { Id = conversationId, ProjectId = Guid.NewGuid(), TagNo = "TAG-2" };
        return conversation;
    }

    [Fact]
    public async Task CreateConversation_ThrowsSavingConversationThrowsException()
    {
        var conversation = SetUpConversation();

        _famServiceMock.Setup(x =>
            x.GetTagData(conversation.TagNo))
                .ReturnsAsync(new TagData { TagNo = "TAG-016", Description = "Test Tag" });
        _conversationRepositoryMock.Setup(x =>
            x.CreateConversation(conversation)).ThrowsAsync(new DbUpdateException());

        await Assert.ThrowsAsync<DbUpdateException>(() => _conversationService.CreateConversation(conversation));
    }

    [Fact]
    public async Task CreateConversation_RunsOkayWithCorrectInput()
    {
        var conversation = SetUpConversation();

        _famServiceMock.Setup(x =>
                x.GetTagData(conversation.TagNo))
                    .ReturnsAsync(new TagData { TagNo = "TAG-016", Description = "Test Tag" });
        _conversationRepositoryMock.Setup(x => x.CreateConversation(conversation)).ReturnsAsync(conversation);

        var result = await _conversationService.CreateConversation(conversation);

        Assert.NotNull(result);
        Assert.Equal(conversation.Id, result.Id);
        _conversationRepositoryMock.Verify(x => x.CreateConversation(conversation), Times.Once);
    }

    [Fact]
    public async Task GetConversation_ThrowsWhenFetchingConversationThrowsException()
    {
        var conversation = SetUpConversation();

        _conversationRepositoryMock.Setup(x => x.GetConversation(conversation.Id)).ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() => _conversationService.GetConversation(conversation.Id));
    }

    [Fact]
    public async Task GetConversation_RunsOkayWithCorrectInput()
    {
        var conversation = SetUpConversation();

        _conversationRepositoryMock.Setup(x => x.GetConversation(conversation.Id)).ReturnsAsync(conversation);

        var result = await _conversationService.GetConversation(conversation.Id);

        Assert.NotNull(result);
        Assert.Equal(conversation.Id, result.Id);
        _conversationRepositoryMock.Verify(x => x.GetConversation(conversation.Id), Times.Once);
    }

    [Fact]
    public async Task GetConversations_ThrowsWhenFetchingConversationsThrowsException()
    {
        var conversation = SetUpConversation();

        _conversationRepositoryMock.Setup(x =>
            x.GetConversations(conversation.ProjectId, conversation.TagNo))
                .ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _conversationService.GetConversations(conversation.ProjectId, conversation.TagNo, false));
    }

    [Fact]
    public async Task GetConversations_RunsOkayWithCorrectInput()
    {
        var conversation = SetUpConversation();

        _conversationRepositoryMock.Setup(x =>
            x.GetConversations(conversation.ProjectId, conversation.TagNo))
                .ReturnsAsync(new List<Conversation> { conversation });

        var result = await _conversationService.GetConversations(conversation.ProjectId,
            conversation.TagNo, false);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(conversation.Id, result[0].Id);
        _conversationRepositoryMock.Verify(x =>
            x.GetConversations(conversation.ProjectId, conversation.TagNo), Times.Once);
    }

    [Fact]
    public async Task GetConversationsWithLatestMessage_RunsOkay()
    {
        var conversation = SetUpConversation();

        _conversationRepositoryMock.Setup(x =>
            x.GetConversationsWithLatestMessage(conversation.ProjectId, conversation.TagNo, false))
                .ReturnsAsync(new List<Conversation> { conversation });

        var result = await _conversationService.GetConversations(conversation.ProjectId,
            conversation.TagNo, true);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(conversation.Id, result[0].Id);
        _conversationRepositoryMock.Verify(x =>
            x.GetConversationsWithLatestMessage(conversation.ProjectId, conversation.TagNo, false), Times.Once);
    }

    [Fact]
    public async Task AddMessage_ThrowsWhenUnableToFetchConversation()
    {
        var message = SetUpMessage();
        _conversationRepositoryMock.Setup(x => x.GetConversation(message.ConversationId))
                .ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(() => _conversationService
                .AddMessage(message.ConversationId, message));
    }

    [Fact]
    public async Task AddMessage_ThrowsWhenConversationIdNotfound()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetConversation(message.ConversationId))
                    .ReturnsAsync((Conversation?)null);

        await Assert.ThrowsAsync<Exception>(() => _conversationService
                    .AddMessage(message.ConversationId, message));
    }

    [Fact]
    public async Task AddMessage_ThrowsWhenSavingMessageThrowsException()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetConversation(message.ConversationId))
                .ReturnsAsync(SetUpConversation());
        _conversationRepositoryMock.Setup(x => x.AddMessage(message)).ThrowsAsync(new DbUpdateException());

        await Assert.ThrowsAsync<DbUpdateException>(() => _conversationService
            .AddMessage(message.ConversationId, message));
    }

    [Fact]
    public async Task AddMessage_RunsOkayWithCorrectInput()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetConversation(message.ConversationId))
                    .ReturnsAsync(SetUpConversation());
        _conversationRepositoryMock.Setup(x => x.AddMessage(message)).ReturnsAsync(message);

        var result = await _conversationService.AddMessage(message.ConversationId, message);

        Assert.NotNull(result);
        Assert.Equal(message.Id, result.Id);
        _conversationRepositoryMock.Verify(x => x.AddMessage(message), Times.Once);
    }

    [Fact]
    public async Task GetMessage_ThrowsWhenFetchingMessageThrowsException()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id)).ThrowsAsync(new ArgumentNullException());

        await Assert.ThrowsAsync<ArgumentNullException>(() => _conversationService.GetMessage(message.Id));
    }

    [Fact]
    public async Task GetMessage_RunsOkayWithCorrectInput()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id)).ReturnsAsync(message);

        var result = await _conversationService.GetMessage(message.Id);

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
            _conversationService.GetMessages(conversationId));
    }

    [Fact]
    public async Task GetMesages_RunsOkayWithCorrectInput()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessages(message.ConversationId))
                    .ReturnsAsync(new List<Message> { message });

        var result = await _conversationService.GetMessages(message.ConversationId);

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
                _conversationService.DeleteMessage(message.Id, Guid.Empty));
    }

    [Fact]
    public async Task DeleteMessage_ThrowsNotFoundExceptionWhenMessageIsNotFound()
    {
        // Arrange
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id))
                    .ReturnsAsync((Message?)null);
        await Assert.ThrowsAsync<NotFoundException>(() =>
                _conversationService.DeleteMessage(message.Id, message.UserId));
    }

    [Fact]
    public async Task DeleteMessage_ThrowsException_WhenDeletingUserNotSameAsCreatedUser()
    {
        // Arrange
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id))
                    .ReturnsAsync(message);
        await Assert.ThrowsAsync<BadRequestException>(() =>
                _conversationService.DeleteMessage(message.Id, Guid.NewGuid()));
    }

    [Fact]
    public async Task DeleteMessage_RunsOk()
    {
        var message = SetUpMessage();

        _conversationRepositoryMock.Setup(x => x.GetMessage(message.Id)).ReturnsAsync(message);
        _conversationRepositoryMock.Setup(x => x.UpdateMessage(message));

        await _conversationService.DeleteMessage(message.Id, message.UserId);

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

        var result = await _conversationService.UpdateMessage(message.Id, message);

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
                _conversationService.UpdateMessage(message.Id, updatedMessage));
    }
}
