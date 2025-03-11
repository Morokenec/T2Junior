using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
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
            public string ChatName { get; set; } = "Дмитрий Ушаков";
            public int IdType { get; set; }
            public enum TypeName
            {
                Все,
                Личные,
                Сообщества
            }
            public TypeName Type { get; set; } = TypeName.Личные;
            public string Photo { get; set; } = "profile_placeholder.svg";
            public int IdUser { get; set; }
            public string Body { get; set; } = "Сообщение";
            public int UnreadCount { get; set; } = 1;
        }
    }
}
