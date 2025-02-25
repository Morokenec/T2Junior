namespace T2WebSoket.Models
{
    public class User : BaseCommonProperties
    {
        public string UserName { get; set; }

        public virtual ICollection<UsersChats> UserChats { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
