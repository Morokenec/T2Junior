using MauiApp1.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels
{
    /// <summary>
    /// ViewModel для управления заметками.
    /// </summary>
    /// <remarks>
    /// Предоставление методов для фильтрации и отображения заметок.
    /// </remarks>
    public class NoteViewModel : BindableObject
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

        /// <summary>
        /// Полный список заметок.
        /// </summary>
        public ObservableCollection<Note> Notes { get; set; }

        /// <summary>
        /// Отфильтрованный список заметок.
        /// </summary>
        public ObservableCollection<Note> FilteredNotes { get; set; }

        private Note _selectedNote;

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

        /// <summary>
        /// Конструктор класса NoteViewModel.
        /// </summary>
        public NoteViewModel()
        {
            //LoadDetailedNote();
            Notes = new ObservableCollection<Note>
        {
            new Note { Id = new Guid().ToString(),
                Name = "День защитника Отечества",
                Description = "«День защитника Отечества» — праздник, отмечаемый ежегодно 23 февраля в Белоруссии, Кыргызстане, России, Таджикистане и непризнанной ПМР.ие проекта",
                 },
            new Note { Id = new Guid().ToString(), Name = "НазваниеНовости", Description = "ТекстНовости"},
            new Note { Id = new Guid().ToString() }
        };

            FilteredNotes = new ObservableCollection<Note>(Notes);
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

        /// <summary>
        /// Получение заметки по идентификатору.
        /// </summary>
        /// <param name="idNote">Идентификатор заметки.</param>
        /// <returns>Заметка с заданным идентификатором или null, если не найдена.</returns>
        public Note GetNoteById(string idNote)
        {
            return Notes.FirstOrDefault(c => c.Id == idNote);
        }
    }
}