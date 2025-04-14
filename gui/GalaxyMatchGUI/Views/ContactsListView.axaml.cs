using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GalaxyMatchGUI.Services;
using GalaxyMatchGUI.ViewModels;
namespace GalaxyMatchGUI.Views;

public partial class ContactsListView : UserControl
{
    public ContactsListView()
    {
        InitializeComponent();
        DataContext = new ContactsListViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}