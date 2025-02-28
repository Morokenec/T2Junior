namespace T2JuniorAPI.Entities
{
    public class InitiativeComment : BaseCommonProperties
    {
        public Guid IdInitiative { get; set; }
        public Guid IdUser { get; set; }
        public string Text { get; set; }

        public virtual Initiative Initiative { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
