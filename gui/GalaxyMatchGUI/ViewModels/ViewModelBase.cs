using CommunityToolkit.Mvvm.ComponentModel;
using GalaxyMatchGUI.Services;
using System;

namespace GalaxyMatchGUI.ViewModels;

public class ViewModelBase : ObservableObject
{
    protected INavigationService? NavigationService => 
        App.ServiceProvider?.GetService(typeof(INavigationService)) as INavigationService;
}
