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
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "КофеКлуб";

        [JsonPropertyName("target")]
        public string Target { get; set; } = "Клуб кофеманов";

        [JsonPropertyName("avatarPath")]
        public string AvatarPath { get; set; } = "club_placeholder.svg";

        [JsonPropertyName("usersCount")]
        public int UsersCount { get; set; } = 13;

        [JsonPropertyName("isUserSubscribed")]
        public bool IsUserSubscribed { get; set; }

        [NotMapped]
        public bool IsVisibileSubscribeButton => !IsUserSubscribed;
    }
}