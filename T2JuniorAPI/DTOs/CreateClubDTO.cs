namespace T2JuniorAPI.DTOs
{
    public class CreateClubDTO
    {
        public string Name { get; set; } = null!;

        public string Rules { get; set; } = null!;

        public string Target { get; set; } = null!;

        public string CreatorUserId { get; set; } = null!;
    }
}
