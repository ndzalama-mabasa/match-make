using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GalaxyMatchGUI.Services;
using CommunityToolkit.Mvvm.Input;

namespace GalaxyMatchGUI.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isLoggingIn;

        [ObservableProperty]
        private string _statusMessage = string.Empty;

        public LoginViewModel()
        {
        }

        [RelayCommand]
        public async Task GoogleSignInAsync()
        {
            try
            {
                IsLoggingIn = true;
                StatusMessage = "Signing in with Google...";

                var authService = new AuthService();
                await authService.StartLoginFlow();
                
                // Navigate to the matching view using our navigation service
                NavigationService?.NavigateTo<MatchingViewModel>();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Sign-in failed: {ex.Message}";
            }
            finally
            {
                IsLoggingIn = false;
            }
        }
    }
}