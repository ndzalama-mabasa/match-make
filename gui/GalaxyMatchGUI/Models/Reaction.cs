using System;
using System.Threading.Tasks;
using GalaxyMatchGUI.ViewModels;

namespace GalaxyMatchGUI.Models;

public class Reaction
{
    ReactionsViewModel _reactionsViewModel;
    public Reaction(ReactionsViewModel reactionsViewModel)
    {
        _reactionsViewModel = reactionsViewModel;
    }

    public Reaction(){}

    public void SetContactsViewModel(ReactionsViewModel reactionsViewModel)
    {
        _reactionsViewModel = reactionsViewModel;
    }
    public Guid UserId { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }

    public async Task AcceptRequest()
    {
        await _reactionsViewModel.AcceptRequestAsync(this);
    }
    
    public async Task CancelRequest()
    {
        await _reactionsViewModel.CancelRequestAsync(this);
    }
}