using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Models
{
    public class UserSubscribers
    {
        public string IdUser { get; set; }
        public ApplicationUser User { get; set; }

        public string IdSubscriber { get; set; }
        public ApplicationUser Subscriber { get; set; }
    }
}
