namespace T2JuniorAPI.DTOs
{
    public class UnsubscribeUserDTO
    {
        public required Guid UserId { get; set; }

        public required Guid SubscriptionId { get; set; }
    }
}
