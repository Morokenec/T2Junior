using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models
{
    public class UserSubscribers
    {
        [Required]
        public int IdUser { get; set; }

        [Required]
        public int IdSubscriber { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<User> Subscriber { get; set; } = new List<User>();
    }
}
