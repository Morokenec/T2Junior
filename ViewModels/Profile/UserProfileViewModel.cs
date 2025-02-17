using MauiApp1.Models.Profile;
using MauiApp1.Services.UseCase.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Profile
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        private readonly IProfileService _taiyoService;
        private UserInfo _userInfo;
        private string _pathAvatarUser;

        public UserProfileViewModel(IProfileService taiyoService)
        {
            _taiyoService = taiyoService;
            // Создаем команду с асинхронной обработкой
            LoadDataCommand = new Command(async () => await LoadDataAsync());
        }

        public ICommand LoadDataCommand { get; }

        public UserInfo UserInfo
        {
            get => _userInfo;
            set
            {
                if (_userInfo != value)
                {
                    _userInfo = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PathAvatarUser
        {
            get => _pathAvatarUser;

            set
            {
                if(_pathAvatarUser != value)
                {
                    _pathAvatarUser = $"t2.hahatun.fun/{value}"; ;
                    OnPropertyChanged();
                }
            }
        }

        public async Task LoadDataAsync()
        {
            var response = await _taiyoService.GetProfileDataAsync();
            if (response?.Result != null)
            {
                UserInfo = response.Result;
                PathAvatarUser = response.Result.AvatarPath;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
