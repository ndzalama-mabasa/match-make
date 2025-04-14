using System;

namespace GalaxyMatchGUI.Models;

public class Contact
{
    public Guid UserId { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }
}