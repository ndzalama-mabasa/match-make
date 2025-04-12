using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GalaxyMatchGUI.ViewModels;

namespace GalaxyMatchGUI.Views
{
    public partial class LoginView : UserControl
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