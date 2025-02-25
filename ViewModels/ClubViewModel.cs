using MauiApp1.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModel
{
    public class ClubViewModel : BindableObject
    {
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
        public ObservableCollection<Club> Clubs { get; set; }
        public ObservableCollection<Club> FilteredClubs { get; set; }
        public ICommand SubscriptionCheckCommand { get; }

        private bool _isSubscribed;
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                _isSubscribed = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public string SubImageSource => IsSubscribed ? "already_subbed.svg" : "add_a_new.svg";

        public ClubViewModel()
        {
            Clubs = new ObservableCollection<Club>
        {
            new Club { IdClub = 1, SubCount = 10200, Rating = 5},
            new Club { IdClub = 2, SubCount = 13000000, Rating = 13},
            new Club { IdClub = 3 },
            new Club { IdClub = 4 },
            new Club { IdClub = 5 },
            new Club { IdClub = 6 },
            new Club { IdClub = 7 },
            new Club { IdClub = 8 },
            new Club { IdClub = 9 }
        };

            FilteredClubs = new ObservableCollection<Club>(Clubs);
            SubscriptionCheckCommand = new Command(ToggleSubscription);
        }

        public void FilterClubs()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredClubs.Clear();
                foreach (var club in Clubs)
                {
                    FilteredClubs.Add(club);
                }
            }
            else
            {
                var filtered = Clubs.Where(m => m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                FilteredClubs.Clear();
                foreach (var club in filtered)
                {
                    FilteredClubs.Add(club);
                }
            }
        }

        private void ToggleSubscription()
        {
            IsSubscribed = !IsSubscribed;
        }

        public Club GetClubById(int idClub)
        {
            return Clubs.FirstOrDefault(c => c.IdClub == idClub);
        }
    }
}