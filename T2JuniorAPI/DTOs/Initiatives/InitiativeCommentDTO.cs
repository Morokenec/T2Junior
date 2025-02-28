namespace T2JuniorAPI.DTOs.Initiatives
{
    public class InitiativeCommentDTO
    {
        public Guid IdUser { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }

    }
}
