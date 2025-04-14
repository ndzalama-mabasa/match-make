using System;
using System.Threading.Tasks;
using GalaxyMatchGUI.ViewModels;

namespace GalaxyMatchGUI.Models;

public class Contact
{
    ContactsViewModel _contactsViewModel;
    public Contact(ContactsViewModel contactsViewModel)
    {
        _contactsViewModel = contactsViewModel;
    }
    public Guid UserId { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }

    public async Task AcceptRequest()
    {
        await _contactsViewModel.AcceptRequestAsync(this);
    }
    
    public async Task CancelRequest()
    {
        await _contactsViewModel.CancelRequestAsync(this);
    }
}