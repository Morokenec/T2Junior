using System.Text.Json.Serialization;

namespace MauiApp1.Models.Profile
{
    public class UserInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("roleName")]
        public string RoleName { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("postAndOrganization")]
        public string PostAndOrganization { get; set; }

        [JsonPropertyName("subscibersCount")] // В JSON опечатка, исправлять её в коде не будем
        public int SubscribersCount { get; set; }

        [JsonPropertyName("subscriptionsCount")]
        public int SubscriptionsCount { get; set; }

        [JsonPropertyName("clubsCount")]
        public int ClubsCount { get; set; }

        [JsonPropertyName("avatarPath")]
        public string AvatarPath { get; set; }
    }
}