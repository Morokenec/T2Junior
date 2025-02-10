using System.ComponentModel.DataAnnotations;

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

        public virtual ApplicationUser IdUserNavigation { get; set; } = null!;

        public virtual MediaType IdMediaTypesNavigation { get; set; } = null!;
        public virtual MediaComment MediaComment { get; set; } = null!;
        public virtual MediaNote MediaNote { get; set; } = null!;
        //public virtual ICollection<MediaNote> MediaNotes { get; set; } = new List<MediaNote>();

        public virtual ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
        public virtual ICollection<MediaEvent> MediaEvents { get; set; } = new List<MediaEvent>();
        public virtual ICollection<MediaClub> MediaClubs { get; set;} = new List<MediaClub>();
    }
}
