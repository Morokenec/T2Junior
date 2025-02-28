namespace T2JuniorAPI.Entities
{
    public class Initiative : BaseCommonProperties
    {
        public Guid IdStatus { get; set; }
        public string IdeaName { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public string Tasks { get; set; }
        public string Budget { get; set; }
        public string Relevance { get; set; }
        public string ExpectedResult { get; set; }

        public virtual InitiativeStatus Status { get; set; }
        public virtual ICollection<UserInitiative> UserInitiatives { get; set; }
        public virtual ICollection<MediaInitiative> MediaInitiatives { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<InitiativeComment> InitiativeComments { get; set; }
    }
}
