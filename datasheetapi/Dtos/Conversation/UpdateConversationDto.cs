using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Dtos.Conversation;
public record UpdateConversationDto
{
    public ConversationStatusDto ConversationStatus { get; set; }
}
