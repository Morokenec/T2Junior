namespace T2JuniorAPI.DTOs
{
    public class UserProfileDTO
    {
        public Guid Id { get; set; }

        public string RoleName { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string PostAndOrganization { get; set; } = null!;

        public int SubscibersCount { get; set; }

        public int SubscriptionsCount { get; set; }
        
        public int ClubsCount { get; set; }
    }
}
