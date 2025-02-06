namespace T2JuniorAPI.DTOs.Clubs
{
    public class CreateClubDTO
    {
        public string Name { get; set; } = null!;

        public string Rules { get; set; } = null!;

        public string Target { get; set; } = null!;

        public Guid CreatorUserId { get; set; }
    }
}
