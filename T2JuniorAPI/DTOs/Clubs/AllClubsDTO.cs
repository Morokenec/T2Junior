namespace T2JuniorAPI.DTOs.Clubs
{
    public class AllClubsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? AvatarPath { get; set; }
        public bool IsSubscribe { get; set; }

    }
}
