using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2JuniorAPI.Entities
{
    public class UserAchievement : BaseCommonProperties
    {
        [Required]
        public required Guid IdUser { get; set; }

        [Required]
        public required Guid IdAchievement { get; set; }
        [ForeignKey("IdUser")]
        public virtual ApplicationUser UserNavigation { get; set; } = null!;
        [ForeignKey("IdAchievement")]
        public virtual Achievement AchievementsNavigation { get; set; } = null!;

        
    }
}
