using MauiApp1.Models.MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels.NoteViewModels
{
    /// <summary>
    /// ViewModel для управления сообщениями.
    /// </summary>
    /// <remarks>
    /// Предоставление методов для фильтрации и отображения сообщений.
    /// </remarks>
    public class MessageViewModel : BindableObject
    {
        private string _searchText;

        /// <summary>
        /// Текст для поиска сообщений.
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
                    FilterChats();
                }
            }
        }

        /// <summary>
        /// Список типов чатов.
        /// </summary>
        public List<string> ChatTypes { get; }

        /// <summary>
        /// Полный список сообщений.
        /// </summary>
        public ObservableCollection<Message> Chats { get; set; }

        /// <summary>
        /// Отфильтрованный список сообщений.
        /// </summary>
        public ObservableCollection<Message> FilteredChats { get; set; }

        /// <summary>
        /// Конструктор класса MessageViewModel.
        /// </summary>
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

        /// <summary>
        /// Фильтрация сообщений по тексту поиска.
        /// </summary>
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

        /// <summary>
        /// Фильтрация сообщений по типу.
        /// </summary>
        /// <param name="type">Тип чата.</param>
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

        /// <summary>
        /// Преобразование строк в список типов чатов.
        /// </summary>
        /// <param name="input">Строка для преобразования.</param>
        /// <returns>Список типов чатов.</returns>
        public List<Message.TypeName> StringToType(string input) => input switch
        {
            "Все" => new List<Message.TypeName> { Message.TypeName.Личные, Message.TypeName.Сообщества },
            "Личные" => new List<Message.TypeName> { Message.TypeName.Личные },
            "Сообщества" => new List<Message.TypeName> { Message.TypeName.Сообщества },
            _ => throw new ArgumentException("Такой категории нет.", nameof(input))
        };

        /// <summary>
        /// Возвращение противоположного типа чата.
        /// </summary>
        /// <param name="input">Строка для преобразования.</param>
        /// <returns>Список типов чатов.</returns>
        public List<Message.TypeName> GetOppositeType(string input) => input switch
        {
            "Личные" => new List<Message.TypeName> { Message.TypeName.Сообщества },
            "Сообщества" => new List<Message.TypeName> { Message.TypeName.Личные },
            "Все" => new List<Message.TypeName> { Message.TypeName.Личные, Message.TypeName.Сообщества },
            _ => throw new ArgumentException("такого нет.", nameof(input))
        };
    }
}
