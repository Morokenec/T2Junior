namespace T2JuniorAPI.DTOs.Medias
{
    public class MediaClubDTO
    {
        public Guid IdClub { get; set; }
        public Guid IdMedia { get; set; }
        public bool IsAvatar { get; set; }
        public string Path { get; set; }
        public string TypeName { get; set; }
    }
}
