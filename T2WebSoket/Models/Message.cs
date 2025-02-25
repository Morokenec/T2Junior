namespace T2WebSoket.Models
{
    public class Message : BaseCommonProperties
    {
        public Guid ChatId { get; set; }
        public Guid UserId  { get; set; }
        public string Body { get; set; }
        public string? MediaUrl { get; set; }
        public virtual Chat Chat { get; set; }
        public virtual User User { get; set; }
    }
}
