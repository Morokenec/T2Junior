using MauiApp1.Models;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    public class UserProfileViewModel : BindableObject
    {
        private UserProfileDTO _selectedProfile;
        private int _selectedProfileId;

        public UserProfileDTO SelectedProfile
        {
            get => _selectedProfile;
            set
            {
                _selectedProfile = value;
                OnPropertyChanged();
            }
        }

        public int SelectedProfileId
        {
            get => _selectedProfileId;
            set
            {
                _selectedProfileId = value;
            }
        }
        public string FullName => SelectedProfile.Name;
        public int Coins => SelectedProfile.AccumulatedPoints;

        public UserProfileViewModel()
        {
            LoadProfile();
        }

        private void LoadProfile()
        {
            UserViewModel userView = new UserViewModel();
            SelectedProfile = userView.GetProfileById(SelectedProfileId);
        }
    }
}
