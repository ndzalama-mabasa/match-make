using System;

namespace GalaxyMatchGUI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public required string MessageContent { get; set; }
        public DateTime SentDate { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public DateTime? ReadAt { get; set; }
        
        // Navigation properties
        public required User Sender { get; set; }
        public required User Recipient { get; set; }
    }
}