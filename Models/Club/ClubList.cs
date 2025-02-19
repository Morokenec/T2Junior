using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiApp1.Models.Club
{
    public class ClubList
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("avatarPath")]
        public string? AvatarPath { get; set; }

        [JsonPropertyName("isSubscribe")]
        public bool IsSubscribe { get; set; }
    }
}
