namespace T2JuniorAPI.DTOs.Comments
{
    public class CreateCommentDTO
    {
        public Guid IdUser { get; set; }
        public string Text { get; set; }
        public Guid? ParrentCommentId { get; set; }
    }
}
