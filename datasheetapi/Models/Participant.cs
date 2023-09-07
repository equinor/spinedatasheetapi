using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Models;

public class Participant
{
    //TODO Fix the cons
    public Participant(Guid userId, Guid conversationId)
    {
        UserId = userId;
        ConversationId = conversationId;
    }

    public Participant()
    {
    }

    public Guid UserId { get; set; }
    public Guid ConversationId { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

}