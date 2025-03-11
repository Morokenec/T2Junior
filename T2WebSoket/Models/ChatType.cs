namespace T2WebSoket.Models
{
    public class ChatType : BaseCommonProperties
    {
        public string Name { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}
