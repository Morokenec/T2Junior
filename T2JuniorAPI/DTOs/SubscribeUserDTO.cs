namespace T2JuniorAPI.DTOs
{
    public class SubscribeUserDTO
    {
        public required Guid UserId { get; set; }

        public required Guid SubscriberId { get; set; }

    }
}
