namespace T2JuniorAPI.DTOs.Medias
{
    public class MediaCommentDTO
    {
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public Guid MediaId { get; set; }
        public string MediaUrl { get; set; }
    }
}
