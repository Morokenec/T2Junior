using MauiApp1.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public class ClubProfileViewModel : BindableObject
    {
        private Club _selectedClub;
        public ObservableCollection<Note> Notes { get; set; }
        public ObservableCollection<Note> FilteredNotes { get; set; }

        public Club SelectedClub
        {
            get => _selectedClub;
            set
            {
                _selectedClub = value;
                OnPropertyChanged();
            }
        }

        public ClubProfileViewModel()
        {
            LoadClubDetails();
            Notes = new ObservableCollection<Note>
        {
            new Note { IdNote = 1 },
            new Note { IdNote = 2 },
            new Note { IdNote = 3 }
        };

            FilteredNotes = new ObservableCollection<Note>(Notes);
        }

        private void LoadClubDetails()
        {
            var clubViewModel = new ClubViewModel();
            SelectedClub = clubViewModel.GetClubById(ClubProfilePage.SelectedClubId);
        }
    }
}
