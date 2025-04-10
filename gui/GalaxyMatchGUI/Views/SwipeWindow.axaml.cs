using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GalaxyMatchGUI;

public partial class SwipeWindow : Window
{
    public SwipeWindow()
    {
        InitializeComponent();
    }

    private void OnSwipeLeft(object sender, System.EventArgs e)
    {

        LoadNextProfile();
    }

    private void OnSwipeRight(object sender, System.EventArgs e)
    {

        LoadNextProfile();
    }

    private void LoadNextProfile()
    {

    }
}