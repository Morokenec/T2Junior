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
    public class ClubProfileViewModel : BindableObject
    {
        private readonly IClubService _clubService;
        private readonly INoteService _noteService;

        private Club _selectedClub;
        private Guid _selectedClubId;

        public ObservableCollection<ClubList> Clubs { get; set; }
        public ObservableCollection<Club> ClubProfiles { get; set; }
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public Club SelectedClub
        {
            get => _selectedClub;
            set
            {
                _selectedClub = value;
                OnPropertyChanged();
            }
        }

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

        public async Task LoadClubsAsync()
        {
            var clubs = await _clubService.GetClubsAsync();
            if (clubs != null)
            {
                Clubs.Clear();
                foreach (var club in clubs)
                {
                    Clubs.Add(club);
                }
            }
         }

        public async Task LoadClubProfileAsync()
        {
            SelectedClub = null;
            var clubProfile = await _clubService.GetClubById(SelectedClubId);
            if (clubProfile != null)
            {
                SelectedClub = clubProfile;
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
    }
}
