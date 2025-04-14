using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalaxyMatchGUI.Models;
using GalaxyMatchGUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace GalaxyMatchGUI.ViewModels;

public class MatchingViewModel : ViewModelBase
{
    private Profile? _currentProfile;
    public Profile? CurrentProfile
    {
        get => _currentProfile;
        set => SetProperty(ref _currentProfile, value);
    }

    private IImage? _avatarImage;
    public IImage? AvatarImage
    {
        get => _avatarImage;
        set => SetProperty(ref _avatarImage, value);
    }

    public ObservableCollection<PhysicalAttribute> PhysicalAttributes { get; } = new();
    public ObservableCollection<InterestItem> Interests { get; } = new();

    public IRelayCommand SwipeLeftCommand { get; }
    public IRelayCommand SwipeRightCommand { get; }
    public IRelayCommand ViewProfileCommand { get; }
    public IRelayCommand ViewMessagesCommand { get; }

    public MatchingViewModel()
    {
        SwipeLeftCommand = new RelayCommand(SwipeLeft);
        SwipeRightCommand = new RelayCommand(SwipeRight);
        ViewProfileCommand = new RelayCommand(ViewProfile);
        ViewMessagesCommand = new RelayCommand(ViewMessages);

        // Load initial profile with base64 image
        _ = LoadProfileWithBase64Image();
    }

    private void SwipeLeft()
    {
        // Handle dislike action
        LoadNextProfile();
    }

    private void SwipeRight()
    {
        // Handle like action
        LoadNextProfile();
    }

    private void ViewProfile()
    {
        // Navigate to profile view
    }

    private void ViewMessages()
    {
        // Navigate to messages view
        NavigationService?.NavigateTo<ContactsListViewModel>();
    }

    private void LoadNextProfile()
    {
        // In a real app, this would fetch the next profile from a service
        _ = LoadProfileWithBase64Image();
    }

    private async Task LoadProfileWithBase64Image()
    {
        // Create a profile with base64 image 
        var profile = CreateBasicProfile();
        
        profile.AvatarUrl = "https://images.unsplash.com/photo-1580923368248-877f237696cd?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
        
        // Check if there's a valid URL in the profile
        bool hasValidUrl = !string.IsNullOrEmpty(profile.AvatarUrl) && 
                          (profile.AvatarUrl.StartsWith("http://") || 
                           profile.AvatarUrl.StartsWith("https://"));
        
        // If there's no valid URL, use the local asset file
        if (!hasValidUrl)
        {
            profile.AvatarUrl = "avares://GalaxyMatchGUI/Assets/alien_profile.png";
        }
        
        CurrentProfile = profile;
        
        // Try to load the image
        try
        {
            if (hasValidUrl && !string.IsNullOrEmpty(profile.AvatarUrl))
            {
                // Load from remote URL
                AvatarImage = await AsyncImageLoader.LoadAsync(profile.AvatarUrl);
            }
            else if (!string.IsNullOrEmpty(profile.AvatarUrl))
            {
                // Load from local asset
                var uri = new Uri(profile.AvatarUrl);
                using var stream = AssetLoader.Open(uri);
                AvatarImage = new Bitmap(stream);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading profile image: {ex.Message}");
            
            // If URL loading fails, try fall back to local asset
            if (hasValidUrl)
            {
                try
                {
                    profile.AvatarUrl = "avares://GalaxyMatchGUI/Assets/alien_profile.png";
                    if (!string.IsNullOrEmpty(profile.AvatarUrl))
                    {
                        var uri = new Uri(profile.AvatarUrl);
                        using var stream = AssetLoader.Open(uri);
                        AvatarImage = new Bitmap(stream);
                    }
                }
                catch (Exception fallbackEx)
                {
                    Console.WriteLine($"Error loading fallback image: {fallbackEx.Message}");
                }
            }
        }

        // Load other profile data
        LoadProfileData();
    }

    private Profile CreateBasicProfile()
    {
        // Create a user first without requiring a profile
        var user = new User { 
            OauthId = "mock-oauth-id",
            Id = Guid.NewGuid() 
        };
        
        // Create the profile with a reference to the user
        var profile = new Profile
        {
            DisplayName = "Zorb",
            Bio = "Greetings earthlings! I enjoy exploring the cosmos and collecting rare minerals. Looking for a companion who appreciates the beauty of nebulae and doesn't mind my occasional tentacle shedding.",
            GalacticDateOfBirth = 8750, // Galactic year
            Species = new Species { SpeciesName = "Zorlaxian" },
            Planet = new Planet { PlanetName = "Nebulon-5" },
            UserId = user.Id
        };
        
        // Set up the bidirectional relationship after both objects are created
        user.Profile = profile;
        profile.User = user;
        
        return profile;
    }

    private void LoadProfileData()
    {
        // Clear and populate physical attributes
        PhysicalAttributes.Clear();
        PhysicalAttributes.Add(new PhysicalAttribute { Icon = "👽", Description = "3 Eyes" });
        PhysicalAttributes.Add(new PhysicalAttribute { Icon = "🦑", Description = "7 Tentacles" });
        PhysicalAttributes.Add(new PhysicalAttribute { Icon = "🌈", Description = "Color-shifting" });
        PhysicalAttributes.Add(new PhysicalAttribute { Icon = "🧠", Description = "Telepathic" });

        // Clear and populate interests
        Interests.Clear();
        Interests.Add(new InterestItem { Name = "Space Exploration" });
        Interests.Add(new InterestItem { Name = "Quantum Physics" });
        Interests.Add(new InterestItem { Name = "Earth Cuisine" });
        Interests.Add(new InterestItem { Name = "Intergalactic Travel" });
        Interests.Add(new InterestItem { Name = "Telepathic Music" });
    }

    // Helper classes for UI display
    public class PhysicalAttribute
    {
        public string Icon { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class InterestItem
    {
        public string Name { get; set; } = string.Empty;
    }
}