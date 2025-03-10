using MauiApp1.DataModel;
using System.ComponentModel.DataAnnotations;

namespace MauiApp1.Models
{
    public class Event
    {
        [Required]
        public int IdEvent { get; set; }
        public static Club Club { get; } = new Club(); //нужны конкретные значения
        public int IdClub { get; set; } = Club.IdClub;
        public int IdDirection { get; set; }
        public string Name { get; set; } = "Событие";
        public string Description { get; set; } = "Описание события";
        public string Date { get; set; } = DateTime.Now.ToString("d");
        public List<string> NumberParticipants { get; set; } = new List<string>();
        public List<string> FactParticipants { get; set; } = new List<string>();
        public List<string> BanParticipants { get; set; } = new List<string>();
        public int Rating = 0;
    }
}
