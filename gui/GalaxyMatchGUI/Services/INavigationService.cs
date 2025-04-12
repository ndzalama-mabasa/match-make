using GalaxyMatchGUI.ViewModels;

namespace GalaxyMatchGUI.Services
{
    public interface INavigationService
    {
        /// <summary>
        /// Navigates to a view based on the provided ViewModel type
        /// </summary>
        /// <typeparam name="T">The type of ViewModel to navigate to</typeparam>
        /// <param name="viewModel">Optional instance of the ViewModel</param>
        void NavigateTo<T>(T? viewModel = null) where T : ViewModelBase;

        /// <summary>
        /// Navigates back to the previous view
        /// </summary>
        bool NavigateBack();
    }
}