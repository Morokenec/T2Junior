using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MauiApp1.DataModel
{
    public class Note
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("idWall")]
        public string IdWall { get; set; }

        [JsonPropertyName("idStatus")]
        public string IdStatus { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("idRepost")]
        public int? IdRepost { get; set; }

        [JsonPropertyName("likeCount")]
        public int LikeCount { get; set; }

        [JsonPropertyName("mediaNotes")]
        public List<object> MediaNotes { get; set; } = new List<object>();
    }
}