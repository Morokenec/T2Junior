using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Models.ClubModels.ClubList;
using MauiApp1.Services.UseCase.Interface;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels.ClubProfileViewModel
{
    public class ClubProfileViewModel : BindableObject
    {
        private readonly IClubService _clubService;
        private Club _selectedClubI;
        private Guid _selectedClubId;

        public ObservableCollection<ClubList> Clubs { get; set; }
        public ObservableCollection<Club> ClubProfiles { get; set; }

        public Club SelectedClub
        {
            get => _selectedClubI;
            set
            {
                _selectedClubI = value;
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

        public ClubProfileViewModel(IClubService clubService)
        {
            _clubService = clubService;
            Clubs = new ObservableCollection<ClubList>();
            ClubProfiles = new ObservableCollection<Club>();
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

        public async Task LoadClubProfileAsync(string clubId)
        {
            var clubProfile = await _clubService.GetClubById(clubId);
            if (clubProfile != null)
            {
                SelectedClub = clubProfile;
            }
        }
    }
}
