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
    /// ViewModel для управления профилем пользователя и его заметками.
    /// </summary>
    /// <remarks>
    /// Предоставление методов для загрузки данных профиля и заметок пользователя, а также для обновления данных.
    /// </remarks>
    public class UserProfileViewModel : INotifyPropertyChanged 
    {
        private readonly INoteService _noteService;
        private readonly IProfileService _profileService;

        private string _pathAvatarUser;
        private bool _isRefreshing;

        /// <summary>
        /// Список заметок пользователя.
        /// </summary>
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        /// <summary>
        /// Отфильтрованный список заметок пользователя.
        /// </summary>
        public ObservableCollection<Note> FilteredNotes { get; set; }

        private UserInfo _userInfo;

        /// <summary>
        /// Команда для загрузки данных.
        /// </summary>
        public ICommand LoadDataCommand { get; }

        /// <summary>
        /// Команда для обновления данных.
        /// </summary>
        public ICommand RefreshCommand { get; }

        /// <summary>
        /// Конструктор класса UserProfileViewModel.
        /// </summary>
        /// <param name="profileService">Сервис для получения данных профиля.</param>
        /// <param name="noteService">Сервис для работы с заметками.</param>
        public UserProfileViewModel(IProfileService profileService, INoteService noteService)
        {
            _noteService = noteService;
            _profileService = profileService;
            LoadDataCommand = new Command(async () => await LoadDataAsync(Guid.Parse(UserInfo.Id)));
            RefreshCommand = new Command(async () => await RefreshDataAsync());
        }

        /// <summary>
        /// Информация о пользователе.
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
        /// Флаг, указывающий, на статус выполнения обновления данных.
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

        public bool IsOutProfile => UserInfo != null && UserInfo.Id != AppSettings.test_user_guid;
        public bool IsYourProfile => !IsOutProfile;


        public async Task LoadDataAsync(Guid userId)
        {
            await LoadProfile(userId);
            await LoadNotes();

        }

        private async Task LoadProfile(Guid userId)
        {
            var response = await _profileService.GetProfileDataAsync(userId);
            if (response?.Result != null)
            {
                UserInfo = response.Result;
                PathAvatarUser = response.Result.AvatarPath;
            }
        }

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

        public async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            Notes.Clear();
            await LoadDataAsync(Guid.Parse(UserInfo.Id));
            IsRefreshing = false;
        }

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
        /// Событие, вызываемое при изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод для вызова события PropertyChanged.
        /// </summary>
        /// <param name="propertyName">Имя изменённого свойства.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
