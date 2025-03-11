namespace T2WebSoket.Models
{
    public class Chat : BaseCommonProperties
    {
        public string Name { get; set; }
        public Guid TypeId { get; set; }
        public virtual ChatType ChatType { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UsersChats> UsersChats { get; set; }
        
    }
}
