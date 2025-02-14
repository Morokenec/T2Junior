using MauiApp1.Models;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    public class UserProfileViewModel : BindableObject
    {
        private UserProfileDTO _selectedProfile;

        public UserProfileDTO SelectedProfile
        {
            get => _selectedProfile;
            set
            {
                _selectedProfile = value;
                OnPropertyChanged();
            }
        }

        public string FullName => SelectedProfile.FullName;
        public int Coins => SelectedProfile.AccumulatedPoints;

        public UserProfileViewModel()
        {
            LoadProfile();
        }

        private void LoadProfile()
        {
            UserViewModel userView = new UserViewModel();
            SelectedProfile = userView.GetProfileById(ProfilePage.SelectedProfileId);
        }
    }
}
