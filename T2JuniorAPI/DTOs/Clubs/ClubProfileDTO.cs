namespace T2JuniorAPI.DTOs.Clubs
{
    public class ClubProfileDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Target { get; set; } = null!;

        public string? AvatarPath { get; set; }

        public int UsersCount { get; set; }
    }
}
