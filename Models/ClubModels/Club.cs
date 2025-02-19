using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiApp1.Models.ClubModels.Club
{
    public class Club
    {
        [JsonPropertyName("id")]
        public Guid id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; } = "КофеКлуб";

        [JsonPropertyName("target")]
        public string target { get; set; } = "Клуб кофеманов";

        [JsonPropertyName("avatarPath")]
        public string avatarPath { get; set; } = "club_placeholder.svg";

        public string SubImageSource => IsSubscribed ? "already_subbed.svg" : "add_a_new.svg";

        [JsonPropertyName("usersCount")]
        public int usersCount { get; set; } = 13;

        [NotMapped]
        private bool _isSubscribed;

        [NotMapped]
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                _isSubscribed = value;
            }
        }
    }
}