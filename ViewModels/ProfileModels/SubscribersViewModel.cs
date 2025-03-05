using MauiApp1.Models.ProfileModels;
using MauiApp1.Services.UseCase.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels.ProfileModels
{
    public class SubscribersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IProfileService _profileService;

        public ObservableCollection<UserSocial> Users { get; set; } = new ObservableCollection<UserSocial>();

        public SubscribersViewModel(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task LoadUsers()
        {
            Users.Clear();
            var notes = await _profileService.GetUserSubscribers(Guid.Parse(AppSettings.test_user_guid));
            if (notes != null)
            {
                foreach (var note in notes)
                {
                    Users.Add(note);
                }
            }
        }
    }
}
