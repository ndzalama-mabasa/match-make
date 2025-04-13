using System.Collections.Generic;

namespace GalaxyMatchGUI.Models
{
    public class Gender
    {
        public Gender()
        {
            Profiles = new HashSet<Profile>();
        }
        
        public int Id { get; set; }
        public required string GenderName { get; set; }
        
        // Navigation property
        public ICollection<Profile> Profiles { get; set; }
    }
}