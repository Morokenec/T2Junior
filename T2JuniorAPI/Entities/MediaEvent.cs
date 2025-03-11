using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities
{
    public class MediaEvent : BaseCommonProperties
    {
        [Required]
        public required Guid IdEvent { get; set; }

        [Required]
        public required Guid IdMedia { get; set; }

        public virtual Event IdEventNavigation { get; set; } = null!;

        public virtual Mediafile MediaFilesNavigation { get; set; } = null!;
    }
}
