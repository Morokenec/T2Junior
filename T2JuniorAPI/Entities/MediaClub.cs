using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities
{
    public class MediaClub : BaseCommonProperties
    {
        [Required]
        public required Guid IdClub { get; set; }

        [Required]
        public required Guid IdMedia { get; set; }

        [Required]
        public bool IsAvatar { get; set; } = false;

        public virtual Club IdClubNavigation { get; set; } = null!;

        public virtual Mediafile MediaFilesNavigation { get; set; } = null!;

    }
}
