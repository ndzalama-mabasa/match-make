using System;

namespace GalaxyMatchGUI.Models
{
    public class UserInterest
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int InterestId { get; set; }
        
        // Navigation properties
        public required User User { get; set; }
        public required Interest Interest { get; set; }
    }
}