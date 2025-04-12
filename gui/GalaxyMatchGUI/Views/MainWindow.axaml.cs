using Avalonia.Controls;

namespace GalaxyMatchGUI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    // Rename to avoid conflict with the auto-generated property
    public ContentControl? GetContentArea => this.FindControl<ContentControl>("ContentArea");
}