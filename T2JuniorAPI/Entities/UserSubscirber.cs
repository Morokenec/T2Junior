using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.Entities
{
    public class UserSubscribers : BaseCommonProperties
    {
        public required Guid IdUser { get; set; }
        public ApplicationUser User { get; set; }

        public required Guid IdSubscriber { get; set; }
        public ApplicationUser Subscriber { get; set; }
    }
}
