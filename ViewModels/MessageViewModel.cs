using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels
{
    public class MessageViewModel : BindableObject
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
                }
            }
        }
        public List<Message> ChatTypes { get; }

        public ObservableCollection<Message> Chats { get; set; }

        public ObservableCollection<Message> FilteredChats { get; set; }

        public MessageViewModel()
        {
            Chats = new ObservableCollection<Message>
        {
            new Message { IdChat = 1, Type = Message.TypeName.Личные, UnreadCount = 3},
            new Message { IdChat = 2 },
            new Message { IdChat = 3 },
            new Message { IdChat = 4 },
            new Message { IdChat = 5 },
            new Message { IdChat = 6 },
            new Message { IdChat = 7 },
            new Message { IdChat = 8 },
            new Message { IdChat = 9 }
        };

            FilteredChats = new ObservableCollection<Message>(Chats);
            ChatTypes = Enum.GetValues(typeof(Message.TypeName)).Cast<Message>().ToList();
        }

        public void FilterChats()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredChats.Clear();
                foreach (var chat in Chats)
                {
                    FilteredChats.Add(chat);
                }
            }
            else
            {
                var filtered = Chats.Where(m => m.ChatName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                FilteredChats.Clear();
                foreach (var chat in filtered)
                {
                    FilteredChats.Add(chat);
                }
            }
        }
    }
}
