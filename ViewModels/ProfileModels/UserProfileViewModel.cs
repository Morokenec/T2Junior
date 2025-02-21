using MauiApp1.DataModel;
using MauiApp1.Models.Profile;
using MauiApp1.Services.UseCase.Interface;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Profile
{
    public class UserProfileViewModel : INotifyPropertyChanged 
    {
        private readonly INoteService _noteService;
        private readonly IProfileService _profileService;

        private string _pathAvatarUser;
        private bool _isRefreshing;

        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
        public ObservableCollection<Note> FilteredNotes { get; set; }

        private UserInfo _userInfo;

        public ICommand LoadDataCommand { get; }
        public ICommand RefreshCommand { get; }

        public UserProfileViewModel(IProfileService profileService, INoteService noteService)
        {
            _noteService = noteService;
            _profileService = profileService;
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            RefreshCommand = new Command(async () => await RefreshDataAsync());
        }

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
            await LoadProfile();
            await LoadNotes();

        }

        private async Task LoadProfile()
        {
            var response = await _profileService.GetProfileDataAsync();
            if (response?.Result != null)
            {
                UserInfo = response.Result;
                PathAvatarUser = response.Result.AvatarPath;
            }
        }

        public async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            Notes.Clear();
            await LoadDataAsync();
            IsRefreshing = false;
        }

        private async Task LoadNotes()
        {
            var notes = await _noteService.GetNotesAsync();
            if (notes != null)
            {
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
