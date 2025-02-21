using T2JuniorAPI.DTOs.Medias;

namespace T2JuniorAPI.DTOs.Comments
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public Guid NoteId { get; set; }
        public string UserName { get; set; }
        public string UserAvatarUrl { get; set; }
        public string Text { get; set; }
        public Guid? ParrentCommentId { get; set; }
        public int LikeCount { get; set; }
        public int SubCommentsCount { get; set; }
        public IEnumerable<CommentDTO> SubComments { get; set; }
        public IEnumerable<MediaCommentDTO> MediaComments { get; set; }
    }
}
