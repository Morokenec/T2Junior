using MauiApp1.Models;
using MauiApp1.Pages;
using System.Collections.ObjectModel;

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
        private Chat _selectedChat;

        public Chat SelectedChat
        {
            get => _selectedChat;
            set
            {
                _selectedChat = value;
                OnPropertyChanged();
            }
        }

        private bool _visibility;

        public bool Visibility
        {
            get => _visibility;
            private set
            {
                if (_visibility != value)
                {
                    _visibility = value;
                    OnPropertyChanged(nameof(Visibility));
                }
            }
        }

        public List<string> ChatTypes { get; }

        public ObservableCollection<Chat> Chats { get; set; }

        public ObservableCollection<Message> Messages { get; set; }

        public ObservableCollection<Chat> FilteredChats { get; set; }

        public ObservableCollection<Message> FilteredMessages { get; set; }

        public MessageViewModel()
        {
            Chats = new ObservableCollection<Chat>
            {
                new Chat { IdChat = 1, Type = Chat.TypeName.Личные, UnreadCount = 3},
                new Chat { IdChat = 2, Type = Chat.TypeName.Личные },
                new Chat { IdChat = 3, ChatName = "КофеКлуб", Photo = "club_placeholder.svg", Type = Chat.TypeName.Сообщества },
                new Chat { IdChat = 4, ChatName = "КофеКлуб", Photo = "club_placeholder.svg", Type = Chat.TypeName.Сообщества },
                new Chat { IdChat = 5, ChatName = "КофеКлуб", Photo = "club_placeholder.svg", Type = Chat.TypeName.Сообщества, UnreadCount = 7 },
                new Chat { IdChat = 6 },
                new Chat { IdChat = 7, ChatName = "КофеКлуб", Photo = "club_placeholder.svg", Type = Chat.TypeName.Сообщества },
                new Chat { IdChat = 8, ChatName = "КофеКлуб", Photo = "club_placeholder.svg", Type = Chat.TypeName.Сообщества },
                new Chat { IdChat = 9 }
            };
            Messages = new ObservableCollection<Message>
            {
                new Message {IdChat = 1, Body = "Дизайн сообщения"},
                new Message {IdChat = 2, Body = "СообщениеСообщение"},
                new Message {IdChat = 3, Body = "Сообщение"}
            };

            foreach (Chat chat in Chats)
            {
                UpdateVisibility(chat);
                chat.CountIsVisible = Visibility;
            }

            FilteredChats = new ObservableCollection<Chat>(Chats);
            FilteredMessages = new ObservableCollection<Message>(Messages);
            ChatTypes = Enum.GetValues(typeof(Chat.TypeName)).Cast<Chat.TypeName>().Select(t => t.ToString()).ToList();
        }

        private void UpdateVisibility(Chat chat)
        {
            Visibility = chat.UnreadCount != 0;
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

        public void FilterChatsByType(Chat.TypeName type)
        {
            FilteredChats.Clear();

            if (type == Chat.TypeName.Все)
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

        public List<Chat.TypeName> StringToType(string input) => input switch
        {
            "Все" => new List<Chat.TypeName> { Chat.TypeName.Личные, Chat.TypeName.Сообщества },
            "Личные" => new List<Chat.TypeName> { Chat.TypeName.Личные },
            "Сообщества" => new List<Chat.TypeName> { Chat.TypeName.Сообщества },
            _ => throw new ArgumentException("Такой категории нет.", nameof(input))
        };

        public List<Chat.TypeName> GetOppositeType(string input) => input switch
        {
            "Личные" => new List<Chat.TypeName> { Chat.TypeName.Сообщества },
            "Сообщества" => new List<Chat.TypeName> { Chat.TypeName.Личные },
            "Все" => new List<Chat.TypeName> { Chat.TypeName.Личные, Chat.TypeName.Сообщества },
            _ => throw new ArgumentException("такого нет.", nameof(input))
        };

        private void LoadDialogDetails()
        {
            var dialogViewModel = new MessageViewModel();
            SelectedChat = dialogViewModel.GetChatById(ChatPage.SelectedChatId);
        }

        public Chat GetChatById(int idChat)
        {
            return Chats.FirstOrDefault(c => c.IdChat == idChat);
        }
    }
}
