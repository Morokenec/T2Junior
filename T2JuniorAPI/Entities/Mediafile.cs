using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace T2JuniorAPI.Entities
{
    public class Mediafile : BaseCommonProperties
    {
        [Required]
        public required Guid IdUser { get; set; }

        [Required]
        public required Guid IdType { get; set; }

        [Required]
        public required string Path { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual MediaType IdMediaTypesNavigation { get; set; } = null!;
        [JsonIgnore] 
        public virtual MediaComment MediaComment { get; set; } = null!;
        [JsonIgnore] 
        public virtual MediaNote MediaNote { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
        [JsonIgnore] 
        public virtual ICollection<MediaEvent> MediaEvents { get; set; } = new List<MediaEvent>();
        [JsonIgnore] 
        public virtual ICollection<MediaClub> MediaClubs { get; set;} = new List<MediaClub>();
    }
}
