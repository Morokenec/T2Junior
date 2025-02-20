namespace T2JuniorAPI.Entities
{
    public class CommentLike : BaseCommonProperties
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }

        public virtual Comment Comment{ get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
