using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiApp1.Models.ClubModels.ClubList
{
    public class ClubList
    {
        private string? avatarPath;

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("target")]
        public string Target { get; set; }

        [JsonPropertyName("avatarPath")]
        public string? AvatarPath
        {
            get => avatarPath;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace("wwwroot/", "");
                    value = $"https://t2.hahatun.fun/{value}";
                }
                avatarPath = value;
            }
        }

        [JsonPropertyName("isSubscribe")]
        public bool IsSubscribe { get; set; }
    }
}
