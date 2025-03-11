namespace T2JuniorAPI.Entities
{
    public class UserInitiative : BaseCommonProperties
    {
        public Guid IdInitiative { get; set; }
        public Guid IdUser { get; set; }

        public virtual Initiative Initiative { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
