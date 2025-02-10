using MauiApp1.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    internal class NoteViewModel
    {
        public ObservableCollection<Note> Notes { get; set; }
        public ObservableCollection<Note> FilteredNotes { get; set; }
        public NoteViewModel()
        {
            Notes = new ObservableCollection<Note>
        {
            new Note { IdNote = 1 },
            new Note { IdNote = 2 },
            new Note { IdNote = 3 },
            new Note { IdNote = 4 },
            new Note { IdNote = 5 },
            new Note { IdNote = 6 },
            new Note { IdNote = 7 },
            new Note { IdNote = 8 },
            new Note { IdNote = 9 }
        };

            FilteredNotes = new ObservableCollection<Note>(Notes);
        }
    }
}
