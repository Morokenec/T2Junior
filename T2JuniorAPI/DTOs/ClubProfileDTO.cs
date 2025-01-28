namespace T2JuniorAPI.DTOs
{
    public class ClubProfileDTO
    {
        public string Id { get; set; }

        public string Name { get; set; } = null!;

        public string Target { get; set; } = null!;

        public int UsersCount { get; set; }
    }
}
