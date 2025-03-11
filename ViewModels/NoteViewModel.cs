using MauiApp1.DataModel;
using MauiApp1.Services.UseCase.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    public class NoteViewModel : BindableObject, INotifyPropertyChanged
    {

        private string _searchText;

        /// <summary>
        /// Текст для поиска заметок.
        /// </summary>
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
        public ICommand LoadDataCommand { get; }
        public ICommand RefreshCommand { get; }

        public ObservableCollection<Note> Notes { get; } = new ObservableCollection<Note>();
        public ObservableCollection<Note> FilteredNotes { get; set; }

        private Note _selectedNote;
        private bool _isRefreshing;

        /// <summary>
        /// Выбранная заметка.
        /// </summary>
        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
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

        public NoteViewModel(INoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            Notes.Clear();
            await LoadNotes();
            IsRefreshing = false;
        }

        //private void LoadDetailedNote()
        //{
        //    var viewModel = new NoteViewModel();
        //    SelectedNote = viewModel.GetNoteById(DetailedNotePage.SelectedNoteId);
        //}

        /// <summary>
        /// Фильтрация заметок на основе текста поиска.
        /// </summary>
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
            try
            {
                Notes.Clear();
                var notes = await _noteService.GetNewsAsync();
                if (notes != null)
                {
                    foreach (var note in notes)
                    {
                        Debug.WriteLine($"[DATA] {note.Name}");
                        Notes.Add(note);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR]{ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}