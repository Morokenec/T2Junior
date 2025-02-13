using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Message
    {
        public int IdMessage { get; set; }
        public int IdChat { get; set; }
        public string ChatName { get; set; }
        public int IdType { get; set; }
        public enum TypeName 
        { 
            Все,
            Личные,
            Сообщества
        }
        public Message.TypeName Type { get; set; }
        public string Photo { get; set; } = "avatar_placeholder";
        public int IdUser { get; set; }
        public string Body { get; set; } = "";
        public int UnreadCount { get; set; } = 1;
    }
}
