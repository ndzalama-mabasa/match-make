using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GalaxyMatchGUI.Services;
using GalaxyMatchGUI.ViewModels;
namespace GalaxyMatchGUI.Views;

public partial class ReactionsView : UserControl
{
    public ReactionsView()
    {
        InitializeComponent();
        DataContext = new ReactionsViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}