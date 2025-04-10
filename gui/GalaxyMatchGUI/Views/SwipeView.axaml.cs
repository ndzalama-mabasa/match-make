using Avalonia.Controls;
using GalaxyMatchGUI.ViewModels;

namespace GalaxyMatchGUI;

public partial class SwipeView: Window
{
    public SwipeView()
    {
        InitializeComponent();
        DataContext = new SwipeViewModel();
    }
}