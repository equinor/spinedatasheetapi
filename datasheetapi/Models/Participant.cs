namespace datasheetapi.Models;

public class Participant
{
    public Guid UserId { get; set; }
    public Guid ConversationId { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

}