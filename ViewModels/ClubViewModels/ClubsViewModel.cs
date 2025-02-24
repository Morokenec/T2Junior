using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
using MauiApp1.Models.ClubModels.Club;

namespace MauiApp1.ViewModels.ClubViewModel
{
    /// <summary>
    /// ViewModel для управления списком клубов.
    /// </summary>
    /// <remarks>
    /// Предоставление методов для загрузки, фильтрации и обновления списка клубов, а также для управления подписками на клубы.
    /// </remarks>
    public class ClubsViewModel : BindableObject, INotifyPropertyChanged
    {
        private readonly ClubService _clubService;
        private readonly HttpClient _httpClient;
        private readonly JsonDeserializerService _jsonDeserializerService;
        private bool _isRefreshing;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterClubs();
                }
            }
        }

        private ObservableCollection<Club> _clubs;

        /// <summary>
        /// Текст для поиска клубов.
        /// </summary>
        public ObservableCollection<Club> Clubs
        {
            get => _clubs;
            set
            {
                if (_clubs != value)
                {
                    _clubs = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Club> _filteredClubs;

        /// <summary>
        /// Отфильтрованный список клубов.
        /// </summary>
        public ObservableCollection<Club> FilteredClubs
        {
            get => _filteredClubs;
            set
            {
                if (_filteredClubs != value)
                {
                    _filteredClubs = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Команда для проверки подписки на клуб.
        /// </summary>
        public ICommand SubscriptionCheckCommand { get; }

        /// <summary>
        /// Команда для обновления списка клубов.
        /// </summary>
        public ICommand RefreshCommand { get; }

        private bool _isSubscribed;

        /// <summary>
        /// Флаг, указывающий, подписан ли пользователь на клуб.
        /// </summary>
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                if (_isSubscribed != value)
                {
                    _isSubscribed = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SubImageSource));
                }
            }
        }

        /// <summary>
        /// Флаг, указывающий, на статус выполнения обновления списка клубов.
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

        // Отображение изображения зависит от статуса подписки
        public string SubImageSource => IsSubscribed ? "already_subbed.svg" : "add_a_new.svg";

        /// <summary>
        /// Конструктор класса ClubsViewModel.
        /// </summary>
        public ClubsViewModel()
        {
            _httpClient = new HttpClient();
            _jsonDeserializerService = new JsonDeserializerService();
            _clubService = new ClubService(_httpClient, _jsonDeserializerService);

            Clubs = new ObservableCollection<Club>();
            FilteredClubs = new ObservableCollection<Club>();

            SubscriptionCheckCommand = new Command(ToggleSubscription);

            LoadClubsAsync();
        }

        /// <summary>
        /// Асинхронная загрузка данных о клубах и обновление коллекции.
        /// </summary>
        public async void LoadClubsAsync()
        {
            var clubLists = await _clubService.GetClubsAsync();
            if (clubLists != null)
            {
                // Обновление коллекций на главном потоке
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Clubs.Clear();
                    foreach (var clubItem in clubLists)
                    {
                        var club = new Club
                        {
                            id = clubItem.Id,
                            name = clubItem.Name,
                            IsSubscribed = clubItem.IsSubscribe,
                        };
                        Debug.WriteLine($"[DATA]{club.id} - {club.name} - {club.IsSubscribed} ");
                        Clubs.Add(club);
                    }

                    // Изначально фильтр совпадает с полным списком
                    FilteredClubs.Clear();
                    foreach (var club in Clubs)
                    {
                        FilteredClubs.Add(club);
                    }
                });
            }
            else
            {
                Debug.WriteLine("[ERROR] Не удалось загрузить данные с сервера.");
            }
        }

        /// <summary>
        /// Фильтрация списка клубов по введённому тексту.
        /// </summary>
        public void FilterClubs()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    // Если строка поиска пуста, показываем все клубы
                    FilteredClubs.Clear();
                    foreach (var club in Clubs)
                    {
                        FilteredClubs.Add(club);
                    }
                }
                else
                {
                    var filtered = Clubs.Where(c => c.name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                    FilteredClubs.Clear();
                    foreach (var club in filtered)
                    {
                        FilteredClubs.Add(club);
                    }
                }
            });
        }

        /// <summary>
        /// Переключение состояние подписки.
        /// </summary>
        private void ToggleSubscription()
        {
            IsSubscribed = !IsSubscribed;
        }

        public void RefreshDataAsync()
        {
            IsRefreshing = true;
            LoadClubsAsync();
            IsRefreshing = false;
        }

        /// <summary>
        /// Возвращение клуба по идентификатору.
        /// </summary>
        /// <param name="idClub">Идентификатор клуба.</param>
        /// <returns>Клуб с заданным идентификатором или null, если не найден.</returns>
        public Club GetClubById(Guid idClub)
        {
            return Clubs.FirstOrDefault(c => c.id == idClub);
        }
    }
}
