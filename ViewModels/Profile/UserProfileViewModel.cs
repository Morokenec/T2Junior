using MauiApp1.Models.Profile;
using MauiApp1.Services.UseCase.Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Profile
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        private readonly IProfileService _taiyoService;
        private UserInfo _userInfo;
        private string _pathAvatarUser;
        private bool _isRefreshing;

        public UserProfileViewModel(IProfileService taiyoService)
        {
            _taiyoService = taiyoService;
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            RefreshCommand = new Command(async () => await RefreshDataAsync());
        }

        public ICommand LoadDataCommand { get; }
        public ICommand RefreshCommand { get; }

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
                if (_pathAvatarUser != value)
                {
                    value = value.Replace("wwwroot/", "");
                    _pathAvatarUser = $"t2.hahatun.fun/{value}";
                    OnPropertyChanged();
                }
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
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

        public async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            await LoadDataAsync();
            IsRefreshing = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
