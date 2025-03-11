namespace T2JuniorAPI.DTOs.Achievements
{
    public class AchievementDTO
    {
        public Guid Id { get; set; }
        public Guid MediaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string MediaPath { get; set; }
        public bool IsRecived { get; set; }
    }
}
