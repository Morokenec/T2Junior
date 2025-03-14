﻿using MauiApp1.DataModel;
using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Models.ClubModels.ClubList;
using MauiApp1.Models.Profile;
using MauiApp1.Services.UseCase.Interface;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.ClubProfileViewModel
{
    /// <summary>
    /// ViewModel для профиля клуба.
    /// </summary>
    public class ClubProfileViewModel : BindableObject
    {
        private readonly IClubService _clubService;
        private readonly INoteService _noteService;

        private string _pathClubAvatar;
        private Club _selectedClub;
        private Guid _selectedClubId;

        /// <summary>
        /// Коллекция клубов.
        /// </summary>
        public ObservableCollection<ClubList> Clubs { get; set; }

        /// <summary>
        /// Коллекция профилей клубов.
        /// </summary>
        public ObservableCollection<Club> ClubProfiles { get; set; }

        /// <summary>
        /// Коллекция заметок клуба.
        /// </summary>
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        /// <summary>
        /// Выбранный клуб.
        /// </summary>
        public Club SelectedClub
        {
            get => _selectedClub;
            set
            {
                _selectedClub = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Путь к аватару клуба.
        /// </summary>
        public string PathClubAvatar
        {
            get => _pathClubAvatar;
            set
            {
                if (_pathClubAvatar != value)
                {
                    value = value.Replace("wwwroot/", "");
                    value = $"https://t2.hahatun.fun/{value}";
                    _pathClubAvatar = value;
                    Debug.WriteLine($"[SOURCE]{value}");
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Идентификатор выбранного клуба.
        /// </summary>
        public Guid SelectedClubId
        {
            get => _selectedClubId;
            set
            {
                _selectedClubId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Конструктор ViewModel профиля клуба.
        /// </summary>
        /// <param name="clubService">Сервис клуба</param>
        /// <param name="noteService">Сервис заметок</param>
        public ClubProfileViewModel(IClubService clubService, INoteService noteService)
        {
            _clubService = clubService;
            _noteService = noteService;
            Clubs = new ObservableCollection<ClubList>();
            ClubProfiles = new ObservableCollection<Club>();
        }

        /// <summary>
        /// Конструктор с идентификатором клуба.
        /// </summary>
        /// <param name="clubService">Сервис клуба</param>
        /// <param name="noteService">Сервис заметок</param>
        /// <param name="id">Идентификатор клуба</param>
        public ClubProfileViewModel(IClubService clubService, INoteService noteService, Guid id) : this(clubService, noteService)
        {
            SelectedClubId = id;
        }

        /// <summary>
        /// Загрузка данных профиля клуба.
        /// </summary>
        public async Task LoadClubProfileAsync()
        {
            SelectedClub = null;
            var clubProfile = await _clubService.GetClubById(SelectedClubId);
            if (clubProfile != null)
            {
                SelectedClub = clubProfile;
                PathClubAvatar = clubProfile.AvatarPath;
            }
            await LoadNotes();
        }

        /// <summary>
        /// Загрузка заметок клуба.
        /// </summary>
        private async Task LoadNotes()
        {
            Notes.Clear();
            var notes = await _noteService.GetNotesAsync(SelectedClub.Id);
            if (notes != null)
            {
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        /// <summary>
        /// Подписка на клуб.
        /// </summary>
        public async Task SubscribeClub()
        {
            if (SelectedClub.IsUserSubscribed == false)
            {
                await _clubService.SubscribeClub(SelectedClubId, Guid.Parse(AppSettings.test_user_guid), Guid.Parse(AppSettings.role_id_user_guid));
            }
        }

        /// <summary>
        /// Установка аватара клуба.
        /// </summary>
        public async Task SetAvatarClub()
        {
            try
            {
                var chosenImage = await MediaPicker.PickPhotoAsync();
                if (chosenImage != null)
                {
                    using var stream = await chosenImage.OpenReadAsync();
                    await _clubService.SetAvatarClubUploadServer(SelectedClub.Id, Guid.Parse(AppSettings.test_user_guid), stream);
                    await LoadClubProfileAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] {ex.Message}");
            }
        }
    }
}
