using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models
{
    public class UserSubscribers
    {
        public required string IdUser { get; set; }
        public ApplicationUser User { get; set; }

        public required string IdSubscriber { get; set; }
        public ApplicationUser Subscriber { get; set; }
    }
}
