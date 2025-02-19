namespace T2JuniorAPI.DTOs.Notes
{
    public class NoteDTO
    {
        public Guid Id { get; set; }
        public Guid IdWall { get; set; }
        public Guid IdStatus { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? IdRepost { get; set; }
        public int LikeCount { get; set; }
    }
}
