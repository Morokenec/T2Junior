using MauiApp1.DataModel;
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
    /// ViewModel для управления профилем клуба.
    /// </summary>
    /// <remarks>
    /// Предоставление методов для загрузки списка клубов и профиля выбранного клуба.
    /// </remarks>
    public class ClubProfileViewModel : BindableObject
    {
        private readonly IClubService _clubService;
        private readonly INoteService _noteService;

        private string _pathClubAvatar;
        private Club _selectedClub;
        private Guid _selectedClubId;

        /// <summary>
        /// Список клубов.
        /// </summary>
        public ObservableCollection<ClubList> Clubs { get; set; }

        /// <summary>
        /// Профили клубов.
        /// </summary>
        public ObservableCollection<Club> ClubProfiles { get; set; }
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

        public ClubProfileViewModel(IClubService clubService, INoteService noteService)
        {
            _clubService = clubService;
            _noteService = noteService;
            Clubs = new ObservableCollection<ClubList>();
            ClubProfiles = new ObservableCollection<Club>();
        }

        public ClubProfileViewModel(IClubService clubService, INoteService noteService, Guid id) : this(clubService, noteService)
        {
            SelectedClubId = id;
        }

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

        public async Task SubscribeClub()
        {
            if(SelectedClub.IsUserSubscribed == false)
            {
                await _clubService.SubscribeClub(SelectedClubId, Guid.Parse(AppSettings.test_user_guid), Guid.Parse(AppSettings.role_id_user_guid));
            }
        }

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
