using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace GalaxyMatchGUI.ViewModels
{
    public class ProfileViewModel : ReactiveObject
    {
        public Profile Profile { get; }
        public string DisplayName => Profile.DisplayName;
        public string Bio => Profile.Bio;
        public string AvatarUrl => Profile.AvatarUrl;

        // Optional: lazy-loaded bitmap from URL/local path
        public Bitmap AvatarImage => LoadAvatarImage();

        public string SpeciesName { get; }
        public string PlanetName { get; }
        public string GenderName { get; }
        public ObservableCollection<Interest> Interests { get; } = new ObservableCollection<Interest>();
        public ObservableCollection<UserAttribute> Attributes { get; } = new ObservableCollection<UserAttribute>();

        public ProfileViewModel(Profile profile, string speciesName, string planetName, string genderName, IEnumerable<Interest> interests,
            IEnumerable<UserAttribute> attributes)
        {
            Profile = profile;
            SpeciesName = speciesName;
            PlanetName = planetName;
            GenderName = genderName;
            Interests = new ObservableCollection<Interest>(interests);
            Attributes = new ObservableCollection<UserAttribute>(attributes);
        }

        private Bitmap LoadAvatarImage()
        {
            try
            {
                return new Bitmap(AvatarUrl); // Assumes AvatarUrl is a valid local path or resource URI
            }
            catch
            {
                // TODO: Return a default image on failure
                return null;
            }
        }
    }
}
