using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities
{
    public class MediaNote : BaseCommonProperties
    {
        [Required]
        public required Guid IdNote { get; set; }

        [Required]
        public required Guid IdMedia { get; set; }

        public virtual Mediafile IdMediaNavigation { get; set; } = null!;

        public virtual Note IdNoteNavigation { get; set; } = null!;
    }
}
