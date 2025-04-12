using System;

namespace GalaxyMatchGUI.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public Guid ReactorId { get; set; }
        public Guid TargetId { get; set; }
        public bool IsPositive { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Navigation properties
        public required User Reactor { get; set; }
        public required User Target { get; set; }
    }
}