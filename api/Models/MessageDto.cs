namespace galaxy_match_make.Models;

public class MessageDto
{
    public int Id { get; set; }
    public required string MessageContent { get; set; }
    public DateTime SentDate { get; set; } = DateTime.UtcNow;
    public Guid SenderId { get; set; }
    public Guid RecipientId { get; set; }
}
