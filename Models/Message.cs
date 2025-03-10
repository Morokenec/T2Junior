using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Message
    {
        [Required]
        public int IdMessage { get; set; }

        public static Chat Chat { get; } = new Chat(); //нужны конкретные значения

        public int IdChat { get; set; } = Chat.IdChat;

        public static UserProfileDTO User { get; set; } = new UserProfileDTO(); // нужны конкретные значения

        public int IdUser { get; set; } = User.IdUser;

        public string DialogImage { get; set; } = Chat.Photo;

        public string Name { get; set; } = User.Name; //заменить

        public string Body { get; set; } = "Сообщение";
    }
}
