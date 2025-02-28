using MauiApp1.DataModel;
using MauiApp1.Services.UseCase.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels
{
    public class NoteViewModel : BindableObject, INotifyPropertyChanged
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

        private readonly INoteService _noteService;

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

        public NoteViewModel(INoteService noteService)
        {
            _noteService = noteService;
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

        public async Task LoadNotes()
        {
            Notes.Clear();
            var notes = await _noteService.GetNewsAsync();
            if (notes != null)
            {
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }
    }
}