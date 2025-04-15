using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GalaxyMatchGUI.Services;
using GalaxyMatchGUI.ViewModels;
namespace GalaxyMatchGUI.Views;

public partial class InteractionsView : UserControl
{
    public InteractionsView()
    {
        InitializeComponent();
        DataContext = new InteractionsViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}