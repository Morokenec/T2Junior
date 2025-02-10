using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities
{
    public class Achievement : BaseCommonProperties
    {
        [Required]
        public required Guid IdMedia { get; set; }

        [Required]
        public required string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public virtual Mediafile MediaFilesNavigation { get; set; } = null!;
        public virtual ICollection<UserAchievement> UserAchievement { get; set; } = null!;
    }
}
