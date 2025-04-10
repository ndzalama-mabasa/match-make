using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GalaxyMatchGUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnGoogleSignInClicked(object? sender, RoutedEventArgs e)
        {
            var swipeWindow = new SwipeWindow();

            // Show the new window
            swipeWindow.Show();

            // Optionally, close the current window after navigating
            this.Close();
        }

    }
}