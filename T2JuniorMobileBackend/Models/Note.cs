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

        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("idRepost")]
        public string IdRepost { get; set; }

        [JsonPropertyName("likeCount")]
        public int LikeCount { get; set; }

        [JsonPropertyName("commentsCount")]
        public int CommentsCount { get; set; }

        private List<MediaNote> _mediaNotes;
        [JsonPropertyName("mediaNotes")]
        public List<MediaNote> MediaNotes
        {
            get => _mediaNotes ??= new List<MediaNote>();
            set
            {
                if (_mediaNotes != value)
                {
                    _mediaNotes = value;
                    UpdateMediaHandler();
                }
            }
        }

        private string _mediaHandler;
        [NotMapped]
        public string MediaHandler
        {
            get => _mediaHandler;
            private set
            {
                if (_mediaHandler != value)
                {
                    _mediaHandler = value;
                }
            }
        }

        private void UpdateMediaHandler()
        {
            MediaHandler = MediaNotes?.FirstOrDefault()?.MediaUrl;
        }
    }

    public class MediaNote
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("idNote")]
        public string IdNote { get; set; }

        [JsonPropertyName("idMedia")]
        public string IdMedia { get; set; }

        private string _mediaUrl;
        [JsonPropertyName("mediaUrl")]
        public string MediaUrl
        {
            get => _mediaUrl;
            set
            {
                if (_mediaUrl != value)
                {
                    value = value?.Replace("wwwroot/", "");
                    value = value != null ? $"https://t2.hahatun.fun/{value}" : null;
                    _mediaUrl = value;
                }
            }
        }
    }
}