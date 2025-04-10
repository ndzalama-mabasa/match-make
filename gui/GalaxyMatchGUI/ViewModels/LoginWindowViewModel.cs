using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using GalaxyMatchGUI.Views;
using ReactiveUI;

namespace GalaxyMatchGUI.ViewModels
{
    public class LoginWindowViewModel : ViewModelBase
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        // Command for Google Sign In
        public ICommand GoogleSignInCommand { get; }

        public LoginWindowViewModel()
        {
            // Initialize the command
            GoogleSignInCommand = ReactiveCommand.CreateFromTask(OnGoogleSignInAsync);
        }

        // Method that will be executed when the command is triggered
        private async Task OnGoogleSignInAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Connecting to Google...";

                // Simulate network delay
                await Task.Delay(2000);

                // Implement your Google sign-in logic here
                // For example:
                // bool isAuthenticated = await AuthService.SignInWithGoogleAsync();

                // For now, let's assume authentication was successful
                bool isAuthenticated = true;

                if (isAuthenticated)
                {
                    StatusMessage = "Authentication successful!";

                    // Navigate to main window
                    await Task.Delay(500); // Give user time to see success message

                    // Use Dispatcher to update UI from any thread
                    await Dispatcher.UIThread.InvokeAsync(() => {
                        // Get the current window and navigate
                        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                        {
                            // Close the login window
                            if (desktop.MainWindow is LoginWindow loginWindow)
                            {
                                loginWindow.Close();
                                var mainWindow = new MainWindow();
                                mainWindow.Show();
                            }
                        }
                    });
                }
                else
                {
                    StatusMessage = "Authentication failed. Please try again.";
                }
            }
            catch (System.Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}