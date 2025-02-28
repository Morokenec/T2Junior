namespace T2WebSoket.Models
{
    public class MessageFile : BaseCommonProperties
    {
        public Guid IdMessage { get; set; }
        public Guid IdFile { get; set; }

        public virtual Message Message { get; set; }
        public virtual ChatFile File { get; set; }
    }
}
