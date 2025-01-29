using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models
{
    public class UserSubscribers
    {
        public required string IdUser { get; set; }
        public required ApplicationUser User { get; set; }

        public required string IdSubscriber { get; set; }
        public required ApplicationUser Subscriber { get; set; }
    }
}
