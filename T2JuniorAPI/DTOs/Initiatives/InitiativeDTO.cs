namespace T2JuniorAPI.DTOs.Initiatives
{
    public class InitiativeDTO
    {
        public Guid IdStatus { get; set; }
        public string IdeaName { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public string Tasks { get; set; }
        public string Budget { get; set; }
        public string Relevance { get; set; }
        public string ExpectedResult { get; set; }
    }
}
