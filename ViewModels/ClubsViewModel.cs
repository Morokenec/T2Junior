using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel; 
using Microsoft.Maui.Controls;
using MauiApp1.DataModel;
using MauiApp1.Models;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;

namespace MauiApp1.ViewModel
{
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

        public ICommand SubscriptionCheckCommand { get; }
        public ICommand RefreshCommand { get; }

        private bool _isSubscribed;
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                if (_isSubscribed != value)
                {
                    _isSubscribed = value;
                    OnPropertyChanged();
                    // Обновляем изображение после смены статуса подписки
                    OnPropertyChanged(nameof(SubImageSource));
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

        // Отображаемое изображение зависит от состояния подписки
        public string SubImageSource => IsSubscribed ? "already_subbed.svg" : "add_a_new.svg";

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
        /// Асинхронно загружает данные о клубах и обновляет коллекции.
        /// </summary>
        private async void LoadClubsAsync()
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
                            IdClub = clubItem.Id,
                            Name = clubItem.Name,
                            IsSubscribed = clubItem.IsSubscribe,
                        };
                        Debug.WriteLine($"[DATA]{club.IdClub} - {club.Name} - {club.IsSubscribed} ");
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
        /// Фильтрует список клубов по введённому тексту.
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
                    var filtered = Clubs.Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                    FilteredClubs.Clear();
                    foreach (var club in filtered)
                    {
                        FilteredClubs.Add(club);
                    }
                }
            });
        }

        /// <summary>
        /// Переключает состояние подписки.
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
        /// Возвращает клуб по идентификатору.
        /// </summary>
        /// <param name="idClub">Идентификатор клуба.</param>
        /// <returns>Клуб с заданным идентификатором или null, если не найден.</returns>
        public Club GetClubById(Guid idClub)
        {
            return Clubs.FirstOrDefault(c => c.IdClub == idClub);
        }
    }
}
