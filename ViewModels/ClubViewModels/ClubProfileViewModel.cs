using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Models.ClubModels.ClubList;
using MauiApp1.Services.UseCase.Interface;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

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
        private Club _selectedClubI;
        private Guid _selectedClubId;

        /// <summary>
        /// Список клубов.
        /// </summary>
        public ObservableCollection<ClubList> Clubs { get; set; }

        /// <summary>
        /// Профили клубов.
        /// </summary>
        public ObservableCollection<Club> ClubProfiles { get; set; }

        /// <summary>
        /// Выбранный клуб.
        /// </summary>
        public Club SelectedClub
        {
            get => _selectedClubI;
            set
            {
                _selectedClubI = value;
                OnPropertyChanged();
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
        /// Конструктор класса ClubProfileViewModel.
        /// </summary>
        /// <param name="clubService">Сервис для работы с клубами.</param>
        public ClubProfileViewModel(IClubService clubService)
        {
            _clubService = clubService;
            Clubs = new ObservableCollection<ClubList>();
            ClubProfiles = new ObservableCollection<Club>();
        }

        /// <summary>
        /// Загрузка списка клубов.
        /// </summary>
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

        /// <summary>
        /// Загрузка профиля клуба по его идентификатору.
        /// </summary>
        /// <param name="clubId">Идентификатор клуба.</param>
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
