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

        private void LoadProfile()
        {
            UserViewModel userView = new UserViewModel();
            SelectedProfile = userView.GetProfileById(ClubProfilePage.SelectedClubId);
        }
    }
}
