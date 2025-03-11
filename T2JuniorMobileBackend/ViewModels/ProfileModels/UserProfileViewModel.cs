using MauiApp1.DataModel;
using MauiApp1.Models.Profile;
using MauiApp1.Services.UseCase.Interface;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Profile
{
    /// <summary>
    /// ViewModel для профиля пользователя.
    /// </summary>
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

        /// <summary>
        /// Конструктор ViewModel профиля пользователя.
        /// </summary>
        /// <param name="profileService">Сервис профиля</param>
        /// <param name="noteService">Сервис заметок</param>
        public UserProfileViewModel(IProfileService profileService, INoteService noteService)
        {
            _noteService = noteService;
            _profileService = profileService;
            LoadDataCommand = new Command(async () => await LoadDataAsync(Guid.Parse(UserInfo.Id)));
            RefreshCommand = new Command(async () => await RefreshDataAsync());
        }

        /// <summary>
        /// Данные пользователя.
        /// </summary>
        public UserInfo UserInfo
        {
            get => _userInfo;
            set
            {
                if (_userInfo != value)
                {
                    _userInfo = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsOutProfile));
                    OnPropertyChanged(nameof(IsYourProfile));
                }
            }
        }

        /// <summary>
        /// Путь к аватару пользователя.
        /// </summary>
        public string PathAvatarUser
        {
            get => _pathAvatarUser;
            set
            {
                if (_pathAvatarUser != value)
                {
                    value = value.Replace("wwwroot/", "");
                    value = $"https://t2.hahatun.fun/{value}";
                    _pathAvatarUser = value;
                    Debug.WriteLine($"[SOURCE]{value}");
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Флаг обновления данных.
        /// </summary>
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

        /// <summary>
        /// Проверка, является ли профиль чужим.
        /// </summary>
        public bool IsOutProfile => UserInfo != null && UserInfo.Id != AppSettings.test_user_guid;
        /// <summary>
        /// Проверка, является ли профиль пользователя своим.
        /// </summary>
        public bool IsYourProfile => !IsOutProfile;

        /// <summary>
        /// Загрузка данных профиля и постов.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        public async Task LoadDataAsync(Guid userId)
        {
            await LoadProfile(userId);
            await LoadNotes();
        }

        /// <summary>
        /// Загрузка данных профиля.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        private async Task LoadProfile(Guid userId)
        {
            var response = await _profileService.GetProfileDataAsync(userId);
            if (response?.Result != null)
            {
                UserInfo = response.Result;
                PathAvatarUser = response.Result.AvatarPath;
            }
        }

        /// <summary>
        /// Загрузка постов пользователя.
        /// </summary>
        private async Task LoadNotes()
        {
            Notes.Clear();
            var notes = await _noteService.GetNotesAsync(Guid.Parse(UserInfo.Id));
            if (notes != null)
            {
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            Notes.OrderBy(n => n.CreationDate);
        }

        /// <summary>
        /// Обновление данных профиля и заметок.
        /// </summary>
        public async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            Notes.Clear();
            await LoadDataAsync(Guid.Parse(UserInfo.Id));
            IsRefreshing = false;
        }

        /// <summary>
        /// Установка аватара пользователя.
        /// </summary>
        public async Task SetAvatarProfile()
        {
            try
            {
                var chosenImage = await MediaPicker.PickPhotoAsync();

                if (chosenImage != null)
                {
                    using var stream = await chosenImage.OpenReadAsync();
                    await _profileService.SetAvatarProfileUploadServer(Guid.Parse(AppSettings.test_user_guid), stream);
                    await LoadDataAsync(Guid.Parse(UserInfo.Id));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] {ex.Message}");
            }
        }

        /// <summary>
        /// Событие изменения свойств.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Вызов события изменения свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
