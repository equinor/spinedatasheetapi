namespace datasheetapi.Adapters;
public static class ConversationAdapter
{
    public static Conversation ToModel(this ConversationDto conversationDto,
                            Guid reviewId,
                            Guid azureUniqueId)
    {
        MessageDto messageDto = new()
        {
            Text = conversationDto.Text
        };
        return new Conversation
        {
            Property = conversationDto.Property,
            ConversationLevel = MapConversationLevelDTOToModel(conversationDto.ConversationLevel),
            ConversationStatus = MapConversationStatusDTOToModel(conversationDto.ConversationStatus),
            TagDataReviewId = reviewId,
            Messages = new List<Message> { messageDto.ToMessageModel(azureUniqueId) },
            Participants = new List<Participant> { ToParticipantModel(azureUniqueId) }
        };
    }

    public static GetConversationDto ToDto(this Conversation conversation,
                            Dictionary<Guid, string> userIdNameMap)
    {
        return new GetConversationDto
        {
            Id = conversation.Id,
            CreatedDate = conversation.CreatedDate,
            ModifiedDate = conversation.ModifiedDate,
            Property = conversation.Property,
            ConversationLevel = MapConversationLevelModelToDto(conversation.ConversationLevel),
            ConversationStatus = MapConversationStatusModelToDto(conversation.ConversationStatus),
            Messages = ToMessageDtos(conversation.Messages, userIdNameMap),
            Participants = ToParticipantDtos(conversation.Participants, userIdNameMap),
        };
    }

    public static Message ToMessageModel(
                    this MessageDto messageDto,
                    Guid azureUniqueId)
    {
        return new Message
        {
            Text = messageDto.Text,
            UserId = azureUniqueId,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };
    }

    public static List<GetMessageDto> ToMessageDtos(this List<Message> messages,
                                                    Dictionary<Guid, string> userIdNameMap)
    {
        return messages.Select(message =>
            ToMessageDto(message, userIdNameMap[message.UserId])).ToList();
    }

    public static GetMessageDto ToMessageDto(
                    this Message message,
                    string commenterName)
    {
        return new GetMessageDto
        {
            Id = message.Id,
            Text = message.SoftDeleted ? "" : message.Text,
            UserId = message.UserId,
            CommenterName = commenterName,
            IsEdited = message.IsEdited,
            SoftDeleted = message.SoftDeleted,
            CreatedDate = message.CreatedDate,
            ModifiedDate = message.ModifiedDate
        };
    }

    private static ConversationLevel MapConversationLevelDTOToModel(ConversationLevelDto dto)
    {
        return dto switch
        {
            ConversationLevelDto.Tag => ConversationLevel.Tag,
            ConversationLevelDto.PurchaserRequirement => ConversationLevel.PurchaserRequirement,
            ConversationLevelDto.SupplierOfferedValue => ConversationLevel.SupplierOfferedValue,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), $"Unknown status: {dto}"),
        };
    }

    private static ConversationStatus MapConversationStatusDTOToModel(ConversationStatusDto dto)
    {
        return dto switch
        {
            ConversationStatusDto.Open => ConversationStatus.Open,
            ConversationStatusDto.To_be_implemented => ConversationStatus.To_be_implemented,
            ConversationStatusDto.Closed => ConversationStatus.Closed,
            ConversationStatusDto.Implemented => ConversationStatus.Implemented,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), $"Unknown status: {dto}"),
        };
    }

    private static ConversationLevelDto MapConversationLevelModelToDto(ConversationLevel model)
    {
        return model switch
        {
            ConversationLevel.Tag => ConversationLevelDto.Tag,
            ConversationLevel.PurchaserRequirement => ConversationLevelDto.PurchaserRequirement,
            ConversationLevel.SupplierOfferedValue => ConversationLevelDto.SupplierOfferedValue,
            _ => throw new ArgumentOutOfRangeException(nameof(model), $"Unknown status: {model}"),
        };
    }

    private static ConversationStatusDto MapConversationStatusModelToDto(ConversationStatus model)
    {
        return model switch
        {
            ConversationStatus.Open => ConversationStatusDto.Open,
            ConversationStatus.To_be_implemented => ConversationStatusDto.To_be_implemented,
            ConversationStatus.Closed => ConversationStatusDto.Closed,
            ConversationStatus.Implemented => ConversationStatusDto.Implemented,
            _ => throw new ArgumentOutOfRangeException(nameof(model), $"Unknown status: {model}"),
        };
    }

    private static Participant ToParticipantModel(Guid azureUniqueId)
    {
        return new Participant
        {
            UserId = azureUniqueId,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };
    }

    private static List<UserDto> ToParticipantDtos(this List<Participant> participants,
                                                    Dictionary<Guid, string> userIdNameMap)
    {
        return participants.Select(user =>
            ToParticipantDto(user, userIdNameMap[user.UserId])).ToList();
    }

    private static UserDto ToParticipantDto(
                    Participant participant,
                    string commenterName)
    {
        return new UserDto
        {
            UserId = participant.UserId,
            DisplayName = commenterName
        };
    }
}
