using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GalaxyMatchGUI.ViewModels;

namespace GalaxyMatchGUI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginWindowViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}