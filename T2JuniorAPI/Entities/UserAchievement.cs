using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities
{
    public class UserAchievement
    {
        [Required]
        public required Guid IdUser { get; set; }

        [Required]
        public required Guid IdAchievement { get; set; }

        public virtual ApplicationUser UserNavigation { get; set; } = null!;

        public virtual Achievement AchievementsNavigation { get; set; } = null!;

        
    }
}
