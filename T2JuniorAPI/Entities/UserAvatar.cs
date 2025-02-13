namespace T2JuniorAPI.Entities
{
    public class UserAvatar : BaseCommonProperties
    {
        public required Guid IdUser { get; set; }
        public required Guid IdMedia { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Mediafile Media { get; set; }
    }
}
