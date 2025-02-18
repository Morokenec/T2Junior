using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
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
                    FilterChats();
                }
            }
        }
        public List<string> ChatTypes { get; }

        public ObservableCollection<Message> Chats { get; set; }

        public ObservableCollection<Message> FilteredChats { get; set; }

        public MessageViewModel()
        {
            Chats = new ObservableCollection<Message>
        {
            new Message { IdChat = 1, Type = Message.TypeName.Личные, UnreadCount = 3},
            new Message { IdChat = 2, Type = Message.TypeName.Личные },
            new Message { IdChat = 3, ChatName = "КофеКлуб", Photo = "club_placeholder.svg", Type = Message.TypeName.Сообщества },
            new Message { IdChat = 4, ChatName = "КофеКлуб", Photo = "club_placeholder.svg", Type = Message.TypeName.Сообщества},
            new Message { IdChat = 5, ChatName = "КофеКлуб", Photo = "club_placeholder.svg", Type = Message.TypeName.Сообщества },
            new Message { IdChat = 6 },
            new Message { IdChat = 7, ChatName = "КофеКлуб", Photo = "club_placeholder.svg", Type = Message.TypeName.Сообщества },
            new Message { IdChat = 8, ChatName = "КофеКлуб", Photo = "club_placeholder.svg", Type = Message.TypeName.Сообщества },
            new Message { IdChat = 9 }
        };

            FilteredChats = new ObservableCollection<Message>(Chats);
            ChatTypes = Enum.GetValues(typeof(Message.TypeName)).Cast<Message.TypeName>().Select(t => t.ToString()).ToList();
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

        public void FilterChatsByType(Message.TypeName type)
        {
            FilteredChats.Clear();

            if (type == Message.TypeName.Все)
            {
                foreach (var chat in Chats)
                {
                    FilteredChats.Add(chat);
                }
            }
            else
            {
                var filtered = Chats.Where(m => m.Type == type).ToList();
                foreach (var chat in filtered)
                {
                    FilteredChats.Add(chat);
                }
            }
        }

        public List<Message.TypeName> StringToType(string input) => input switch
        {
            "Все" => new List<Message.TypeName> { Message.TypeName.Личные, Message.TypeName.Сообщества },
            "Личные" => new List<Message.TypeName> { Message.TypeName.Личные },
            "Сообщества" => new List<Message.TypeName> { Message.TypeName.Сообщества },
            _ => throw new ArgumentException("Такой категории нет.", nameof(input))
        };

        public List<Message.TypeName> GetOppositeType(string input) => input switch
        {
            "Личные" => new List<Message.TypeName> { Message.TypeName.Сообщества },
            "Сообщества" => new List<Message.TypeName> { Message.TypeName.Личные },
            "Все" => new List<Message.TypeName> { Message.TypeName.Личные, Message.TypeName.Сообщества },
            _ => throw new ArgumentException("такого нет.", nameof(input))
        };
    }
}
