using T2JuniorAPI.DTOs.Medias;

namespace T2JuniorAPI.DTOs.Initiatives
{
    public class InitiativeOutputDTO
    {
        public Guid Id { get; set; }
        public string IdeaName { get; set; }
        public string StatusName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public string Tasks { get; set; }
        public string Budget { get; set; }
        public string Relevance { get; set; }
        public string ExpectedResult { get; set; }
        public List<InitiativeUserDTO> Team {  get; set; }
        public List<MediafileDTO> Mediafiles { get; set; }
        public int VotesCount { get; set; }
        public List<InitiativeCommentDTO> Comments { get; set; }
    }
}
