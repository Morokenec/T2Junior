namespace T2JuniorAPI.DTOs.Users
{
    public class UnsubscribeUserDTO
    {
        public required Guid UserId { get; set; }

        public required Guid SubscriptionId { get; set; }
    }
}
