namespace T2JuniorAPI.DTOs.Achievements
{
    public class UpdateAchievementDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
