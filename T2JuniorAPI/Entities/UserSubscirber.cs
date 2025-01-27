using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models
{
    public class UserSubscribers
    {
        [Required]
        public int IdUser { get; set; }

        [Required]
        public int IdSubscriber { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public virtual ICollection<ApplicationUser> Subscriber { get; set; } = new List<ApplicationUser>();
    }
}
