using MauiApp1.Models.Profile;
using MauiApp1.Services.UseCase;
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
        private readonly ITaiyoService _taiyoService;

        private UserInfo _userInfo;

        public ICommand LoadDataCommand { get; }

        public UserProfileViewModel(ITaiyoService taiyoService)
        {
            _taiyoService = taiyoService;
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            Debug.WriteLine("[Debug] Остановка");
        }

        public UserInfo UserInfo
        {
            get => _userInfo;
            set
            {
                _userInfo = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadDataAsync()
        {
            var response = await _taiyoService.GetTaiyoDataAsync();
            if (response?.Result != null)
            {
                UserInfo = response.Result;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
