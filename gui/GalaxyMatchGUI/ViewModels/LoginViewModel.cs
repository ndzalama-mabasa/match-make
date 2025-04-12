using System;
using System.Threading.Tasks;
<<<<<<< HEAD
using CommunityToolkit.Mvvm.ComponentModel;
using GalaxyMatchGUI.Services;
using CommunityToolkit.Mvvm.Input;
using GalaxyMatchGUI.Models;
=======
using Avalonia.Controls.ApplicationLifetimes;
using GalaxyMatchGUI.Services;
using ReactiveUI;
>>>>>>> dev

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
<<<<<<< HEAD
                IsLoggingIn = true;
                StatusMessage = "Signing in with Google...";
=======
                IsLoading = true;
                StatusMessage = "Contacting galactic servers...";

                var authService = new AuthService();
                await authService.StartLoginFlow();

                // Simulate login success
                StatusMessage = "Login successful! Navigating through the wormhole...";
>>>>>>> dev

                var authService = new AuthService();
                var authResponse = await authService.StartLoginFlow();
                
                if (authResponse == null)
                {
                    StatusMessage = "Authentication failed. Please try again.";
                    return;
                }

                // Check if the user's profile is complete
                if (authResponse.ProfileComplete)
                {
                    // User already has a profile, navigate to the matching view
                    StatusMessage = "Authentication successful!";
                    NavigationService?.NavigateTo<MatchingViewModel>();
                }
                else
                {
                    // User needs to complete their profile
                    StatusMessage = "Please complete your profile";
                    
                    // TODO: Implement a profile creation view and navigate to it
                    // For now, we'll still navigate to MatchingViewModel
                    NavigationService?.NavigateTo<MatchingViewModel>();
                    
                    // Uncomment when ProfileViewModel is implemented:
                    // NavigationService?.NavigateTo<ProfileViewModel>();
                }
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