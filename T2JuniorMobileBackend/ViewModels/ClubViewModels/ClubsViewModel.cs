﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase;
using MauiApp1.Models.ClubModels.Club;
using CommunityToolkit.Maui.Core.Extensions;

namespace MauiApp1.ViewModels.ClubViewModel
{
    public class ClubsViewModel : BindableObject, INotifyPropertyChanged
    {
        private readonly ClubService _clubService;
        private readonly HttpClient _httpClient;
        private readonly JsonDeserializerService _jsonDeserializerService;
        private bool _isRefreshing;

        private string _searchText;
        private string _pathClubAvatar;
        private bool _isVisibleUserSubscribedClubList;
        private ObservableCollection<Club> _isUserSubscribedClubList;

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
                    OnPropertyChanged(nameof(SubImageSource));
                }
            }
        }

        public bool IsVisibleUserSubscribedClubList
        {
            get => _isVisibleUserSubscribedClubList;
            set
            {
                if (_isVisibleUserSubscribedClubList != value)
                {
                    _isVisibleUserSubscribedClubList = !value;
                    OnPropertyChanged();
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

        public string SubImageSource => IsSubscribed ? "already_subbed.svg" : "add_a_new.svg";

        public ClubsViewModel()
        {
            _httpClient = new HttpClient();
            _jsonDeserializerService = new JsonDeserializerService();
            _clubService = new ClubService(_httpClient, _jsonDeserializerService);

            Clubs = new ObservableCollection<Club>();
            FilteredClubs = new ObservableCollection<Club>();

            SubscriptionCheckCommand = new Command(ToggleSubscription);
        }

        public ClubsViewModel(bool isVisibleUserSubscribedClubList)
        {
            _isVisibleUserSubscribedClubList = isVisibleUserSubscribedClubList;
            _httpClient = new HttpClient();
            _jsonDeserializerService = new JsonDeserializerService();
            _clubService = new ClubService(_httpClient, _jsonDeserializerService);

            Clubs = new ObservableCollection<Club>();
            FilteredClubs = new ObservableCollection<Club>();

            SubscriptionCheckCommand = new Command(ToggleSubscription);
        }

        /// <summary>
        /// Асинхронно загружает данные о клубах и обновляет коллекции.
        /// </summary>
        public async Task LoadClubsAsync()
        {
            var clubLists = await _clubService.GetClubsAsync();
            if (clubLists != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Clubs.Clear();
                    foreach (var clubItem in clubLists)
                    {
                        var club = new Club
                        {
                            Id = clubItem.Id,
                            Name = clubItem.Name,
                            IsUserSubscribed = clubItem.IsSubscribe,
                            AvatarPath = clubItem.AvatarPath,
                            Target = "Спортивный"
                        };
                        Debug.WriteLine($"[DATA]{club.Id} - {club.Name} - {club.IsUserSubscribed} ");
                        Clubs.Add(club);
                    }

                    if (_isVisibleUserSubscribedClubList)
                    {
                        _isUserSubscribedClubList = Clubs.Where(c => c.IsUserSubscribed == true).ToObservableCollection();
                        FilteredClubs.Clear();
                        foreach (var club in _isUserSubscribedClubList)
                        {
                            FilteredClubs.Add(club);
                        }
                    }
                    else
                    {
                        FilteredClubs.Clear();
                        Clubs = new ObservableCollection<Club>(Clubs.OrderBy(c => c.IsVisibileSubscribeButton));
                        foreach (var club in Clubs)
                        {
                            FilteredClubs.Add(club);
                        }
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

        public async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            await LoadClubsAsync();
            IsRefreshing = false;
        }

        /// <summary>
        /// Возвращает клуб по идентификатору.
        /// </summary>
        /// <param name="idClub">Идентификатор клуба.</param>
        /// <returns>Клуб с заданным идентификатором или null, если не найден.</returns>
        public Club GetClubById(Guid idClub)
        {
            return Clubs.FirstOrDefault(c => c.Id == idClub);
        }
    }
}
