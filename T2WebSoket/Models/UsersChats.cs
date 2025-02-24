namespace T2WebSoket.Models
{
    public class UsersChats : BaseCommonProperties
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public virtual Chat Chat { get; set; }
        public virtual User User { get; set; }
    }
}
