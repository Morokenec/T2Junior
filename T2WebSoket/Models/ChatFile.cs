namespace T2WebSoket.Models
{
    public class ChatFile : BaseCommonProperties
    {
        public Guid UserId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<MessageFile> MessageFiles { get; set; }
    }
}
