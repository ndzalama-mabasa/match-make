using System;
using System.Collections.Generic;

namespace GalaxyMatchGUI.Models
{
    public class User
    {
        public User()
        {
            SentReactions = new HashSet<Reaction>();
            ReceivedReactions = new HashSet<Reaction>();
            SentMessages = new HashSet<Message>();
            ReceivedMessages = new HashSet<Message>();
        }

        public Guid Id { get; set; }
        public required string OauthId { get; set; }
        public bool Inactive { get; set; }
        
        // Navigation properties
        public Profile? Profile { get; set; }  // Made optional by removing required keyword and adding nullable
        public ICollection<Reaction> SentReactions { get; set; }
        public ICollection<Reaction> ReceivedReactions { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
    }
}