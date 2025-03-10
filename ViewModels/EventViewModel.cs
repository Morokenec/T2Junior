using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels
{
    public class EventViewModel : BindableObject
    {
        private static EventViewModel? _current;

        public static EventViewModel Current
        {
            get => _current ??= new EventViewModel();
            set => _current = value;
        }

        private int _clickCount;
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
                    FilterEvents();
                }
            }
        }

        private string _selectedEvent;

        public string SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                if (_selectedEvent != value)
                {
                    _selectedEvent = value;
                    OnPropertyChanged(nameof(SelectedEvent));
                }
            }
        }

        private Color _buttonBackgroundColor;

        public Color ButtonBackgroundColor
        {
            get => _buttonBackgroundColor;
            set
            {
                if (_buttonBackgroundColor != value)
                {
                    _buttonBackgroundColor = value;
                    OnPropertyChanged(nameof(ButtonBackgroundColor));
                }
            }
        }

        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<Event> FilteredEvents { get; set; }

        public EventViewModel()
        {
            Events = new ObservableCollection<Event>
            {
                new Event{ IdEvent = 1, Name = "Первая весна...", Date = "01.03.2025"},
                new Event{ },
                new Event{ },
                new Event{ }
            };

            FilteredEvents = new ObservableCollection<Event>(Events);
        }

        private void FilterEvents()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredEvents.Clear();
                foreach (var @event in Events)
                {
                    FilteredEvents.Add(@event);
                }
            }
            else
            {
                var filtered = Events.Where(m => m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                FilteredEvents.Clear();
                foreach (var @event in filtered)
                {
                    FilteredEvents.Add(@event);
                }
            }
        }

        public void OnEventSubRequest(Event @event)
        {
            _clickCount++;

            if (_clickCount % 2 == 0)
            {
                SelectedEvent = string.Empty;
                ButtonBackgroundColor = Colors.White;
            }
            else
            {
                SelectedEvent = @event.Name;
                ButtonBackgroundColor = Color.FromArgb("#0057A6");
            }
        }
    }
}