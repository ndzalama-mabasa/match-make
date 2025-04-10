using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GalaxyMatchGUI.ViewModels;

namespace GalaxyMatchGUI
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}