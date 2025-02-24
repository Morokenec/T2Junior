using MauiApp1.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels
{
    public class NoteViewModel : BindableObject
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
                    FilterNotes();
                }
            }
        }
        public ObservableCollection<Note> Notes { get; set; }
        public ObservableCollection<Note> FilteredNotes { get; set; }

        private Note _selectedNote;

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }

        public NoteViewModel()
        {
            Notes = new ObservableCollection<Note>
            {
                new Note { Id = new Guid().ToString(),
                    Name = "День защитника Отечества",
                    Description = "«День защитника Отечества» — праздник, отмечаемый ежегодно 23 февраля в Белоруссии, Кыргызстане, России, Таджикистане и непризнанной ПМР.ие проекта",
                     },
                new Note { Id = new Guid().ToString(), Name = "НазваниеНовости", Description = "ТекстНовости"}
            };

            FilteredNotes = new ObservableCollection<Note>(Notes);
        }

        //private void LoadDetailedNote()
        //{
        //    var viewModel = new NoteViewModel();
        //    SelectedNote = viewModel.GetNoteById(DetailedNotePage.SelectedNoteId);
        //}

        public void FilterNotes()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredNotes.Clear();
                foreach (var chat in Notes)
                {
                    FilteredNotes.Add(chat);
                }
            }
            else
            {
                var filtered = Notes.Where(m => m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                FilteredNotes.Clear();
                foreach (var chat in filtered)
                {
                    FilteredNotes.Add(chat);
                }
            }
        }

        public Note GetNoteById(string idNote)
        {
            return Notes.FirstOrDefault(c => c.Id == idNote);
        }
    }
}